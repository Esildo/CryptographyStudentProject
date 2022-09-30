using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using RelayBase;
using Relay;
using App.ViewModels.Base;
using Microsoft.Win32;
using CryptoSymm.AsynEnc;
using CryptoSymm.Magenta;
using CryptoSymm.Magenta.Magenta;
using WpfApp2.Servers;
using CryptoSymm;

namespace WpfApp2.MVM
{
    
    internal class MainViewModel : ViewModelBase
    {
        Magentas Magent ;
        Server _Server ;
        MagentKeyGeneration SymKey;
        PublicKey _PublicKey ;
        Benalo Ben;
        Encrypt_File File_Encrypt;


        public MainViewModel()
        {
            Magent = new Magentas();
            _Server = new Server();
            SymKey = new MagentKeyGeneration();
            Ben = new Benalo();
            File_Encrypt = new Encrypt_File();
            _PublicKey = _Server.SendPublicKey();
        }

        private string _filePath;

        public string FilePath
        {
            get => _filePath;
            set
            {
                if (!Set(ref _filePath, value))
                {
                    return;
                }
            }
        }

        private bool _isEnableButtonSavePath = false;

        public bool IsEnableButtonSavePath
        {
            get => _isEnableButtonSavePath;
            set
            {
                if (!Set(ref _isEnableButtonSavePath, value))
                {
                    return;
                }
            }
        }

        private string _fileName;
        public string FileName
        {
            get => _fileName;
            set
            {
                if (!Set(ref _fileName, value))
                {
                    return;
                }
            }
        }
        private bool _isEnableButtonEncrypt = false;

        public bool IsEnableButtonEncrypt
        {
            get => _isEnableButtonEncrypt;
            set
            {
                if (!Set(ref _isEnableButtonEncrypt, value))
                {
                    return;
                }
            }
        }
        private string _filePathSave;
        public string FilePathSave
        {
            get => _filePathSave;
            set
            {
                if (!Set(ref _filePathSave, value))
                {
                    return;
                }
            }
        }

        private bool _isEnableButtonDecrypt = false;

        public bool IsEnableButtonDecrypt
        {
            get => _isEnableButtonDecrypt;
            set
            {
                if (!Set(ref _isEnableButtonDecrypt, value))
                {
                    return;
                }
            }
        }
        private ICommand _encryptMagentCommand;

        public ICommand EncryptMagentCommand => _encryptMagentCommand ??= new RelayCommand(EncryptBlowfish);

        private void EncryptBlowfish(object p)
        {
            if (File.Exists(FilePath))
            {
               File_Encrypt.Encrypt(SymKey,FilePath, @"C:\nikita\Numbers\JoJNEw.txt", 16);
            } 
            IsEnableButtonSavePath = true;
        }



        private ICommand _openDialogFileCommand;
        public ICommand OpenDialogFileCommand => _openDialogFileCommand ??= new RelayCommand(OpenDialogFile);

        private void OpenDialogFile(object p)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                FileName = Path.GetFileName(openFileDialog.FileName);
            if (FileName == null)
            {
                MessageBox.Show("Файл не выбран");
                return;
            }
            FilePath = openFileDialog.FileName;

            IsEnableButtonEncrypt = true;
        }
        private ICommand _chooseSavePathCommand;

        public ICommand ChooseSavePathCommand => _chooseSavePathCommand ??= new RelayCommand(ChooseSavePath);

        private void ChooseSavePath(object p)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt|All files (*.*)|*.*"
            };

            if (saveFileDialog.ShowDialog() == true)
                FilePathSave = saveFileDialog.FileName;
            IsEnableButtonDecrypt = true;
        }

    }
}
