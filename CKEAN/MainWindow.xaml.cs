using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CKEAN
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string ean = textBoxEan.Text;
            int keyCalculate;
            if (Regex.IsMatch(ean, "[0-9]{12}") || Regex.IsMatch(ean, "[0-9]{13}"))
            {
                if (ean.Length == 13)
                {
                    keyCalculate = calculKey((ean.Substring(0, ean.Length - 1)).ToCharArray());
                }
                else
                {
                    keyCalculate = calculKey(ean.ToCharArray());
                }
                key.Content = keyCalculate;
                completeEan.Content = ean + keyCalculate.ToString();
            }
            else
            {
                MessageBox.Show("Le code EAN est incorrect. Veuillez vérifier le nombre de caractère.", "Erreur code EAN", MessageBoxButton.OK);
            }
        }
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            completeEan.Content = "";
            key.Content = "";
            textBoxEan.Text = "";
        }

        private int calculKey(char[] eanWithoutKey)
        {
            int key = 0;
            int somme = 0;
            for (int i = 0; i < eanWithoutKey.Length; i++)
            {
                if ((i+1)%2 == 0)
                {
                    somme += int.Parse(eanWithoutKey[i].ToString())*3;
                }
                else
                {
                    somme += int.Parse(eanWithoutKey[i].ToString());
                }
            }
            MessageBox.Show(somme.ToString());
            if (somme % 10 == 0)
            {
                key = 0;
            }
            else
            {
                key = 10 - (somme % 10);
            }
            return key;
        }

    }
}
