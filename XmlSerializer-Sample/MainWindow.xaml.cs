using Microsoft.Win32;
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
using System.Xml;
using System.Xml.Serialization;
using System.Data.Services;
using System.IO;
using System.Data.Services.Internal;
using System.Windows.Markup;
using System.Diagnostics;

namespace XmlSerializer_Sample
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        //temp elements
        private Dictionary<string, object> _tempElements;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.xml)|*.xml|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                _tempElements = new Dictionary<string, object>();

                string selectedFilePath = openFileDialog.FileName;
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(File.ReadAllText(selectedFilePath));

                foreach (XmlElement xmlItem in xmlDoc.SelectNodes("/root"))
                {
                    string key = xmlItem.GetAttribute("key");
                    string typeName = xmlItem.GetAttribute("type");

                    //Create the XmlSerializer
                    var xser = new XmlSerializer(Type.GetType(typeName));

                    //A reader is needed to read the XML document.
                    var reader = new XmlTextReader(new StringReader(xmlItem.InnerXml));

                    var element = xser.Deserialize(reader);
                    _tempElements.Add(key, element);
                }
            }
        }
    }
}
