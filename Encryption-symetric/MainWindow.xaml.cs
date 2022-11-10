using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Threading;
using System.Diagnostics;

namespace Encryption_symetric
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Stopwatch stopWatch = new();
        byte[]? key;
        byte[]? iv;
        int selected;
        public MainWindow()
        {
            InitializeComponent();
            EncryptionSelector.Selector(selected);
            keyValue.Text = Convert.ToBase64String(EncryptionSelector.algo.Key);
            ivValue.Text = Convert.ToBase64String(EncryptionSelector.algo.IV);
            key = Convert.FromBase64String(keyValue.Text);
            iv = Convert.FromBase64String(ivValue.Text);
        }


        //Selecting the algorithm to use
        private void ComboSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox? comboBox = (ComboBox)sender;
            selected = comboBox.SelectedIndex;
        }

        //Generate Key and IV
        private void GenerateKeyAndIV(object sender, RoutedEventArgs e)
        {
            EncryptionSelector.algo.GenerateKey();
            EncryptionSelector.algo.GenerateIV();
            keyValue.Text = Convert.ToBase64String(EncryptionSelector.algo.Key);
            ivValue.Text = Convert.ToBase64String(EncryptionSelector.algo.IV);
            key = Convert.FromBase64String(keyValue.Text);
            iv = Convert.FromBase64String(ivValue.Text);
        }

        //Encrypting the massage with the cosen algo, key and IV
        private void Encrypt(object sender, RoutedEventArgs e)
        {
            Encrypt encrypt = new();
            string input = inputBox.Text;

            stopWatch.Start();
            (string ascii, string hex) = encrypt.LetsEncryptSomething(selected, input, key, iv);
            TimeSpan encryptionTime = stopWatch.Elapsed;

            encryptedHexOutput.Text = hex;
            encryptedAsciiOutput.Text = ascii;
            keyValue.Text = Convert.ToBase64String(key);
            ivValue.Text = Convert.ToBase64String(iv);
            if (selected > 5)
            {
                encryptedInputASCII.Text = ascii;
            }
            encryptTime.Text = encryptionTime.ToString();
        }

        //Decrypting the massage with the cosen algo, key and IV
        private void Decrypt(object sender, RoutedEventArgs e)
        {
            Decrypt decrypt = new();
            string input = encryptedInputASCII.Text;

            stopWatch.Start();
            (string ascii, string hex) = decrypt.LetsDecryptSomething(selected, input, key, iv);
            TimeSpan decryptionTime = stopWatch.Elapsed;

            outputAscii.Text = ascii;
            outputHEX.Text = hex;
            keyValue.Text = Convert.ToBase64String(key);
            ivValue.Text = Convert.ToBase64String(iv);
            decryptTime.Text = decryptionTime.ToString();
        }

    }
}
