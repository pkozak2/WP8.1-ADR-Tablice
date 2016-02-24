using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;
using System.Xml;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace TabliceADR
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    
    public sealed partial class MainPage : Page
    {
        List<Zagrozenie> zagrozenia = new List<Zagrozenie> { };
        List<Material> materialy = new List<Material> { };

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            lista_zagrozen();
            lista_materialow();

            zagrListBox.ItemsSource = zagrozenia;
            materialListBox.ItemsSource = materialy;

            //messageLabel.Text = "Liczba " + zagrozenia.Count;
            
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        public object lista_zagrozen() 
        {
            XDocument xml = XDocument.Load(@"Data/Data.xml"); 
            zagrozenia = (from q in xml.Elements("zagrozenia").Elements("zagrozenie") 
                                  select new Zagrozenie 
                                  { 
                                      Numer = (string)q.Element("numer").Value, 
                                      Opis = (string)q.Element("opis").Value, 
                                  } 
  
  
            ).ToList(); 
           return zagrozenia; 
        }

        public object lista_materialow()
        {
            XDocument xml = XDocument.Load(@"Data/Data.xml");
            materialy = (from q in xml.Elements("zagrozenia").Elements("material")
                          select new Material
                          {
                              Numer = (string)q.Element("numer").Value,
                              Opis = (string)q.Element("opis").Value,
                          }


            ).ToList();
            return materialy;
        }


        private void zagrSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (zagrozenia != null)
            {
                this.zagrListBox.ItemsSource = zagrozenia.Where(w => w.Numer.Contains(zagrSearch.Text));
            }
        }

        private void materialSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (materialy != null)
            {
                this.materialListBox.ItemsSource = materialy.Where(w => w.Numer.Contains(materialSearch.Text));
            }
        }

        private void aboutAppStart(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(aboutApp));
        } 
    }
}
