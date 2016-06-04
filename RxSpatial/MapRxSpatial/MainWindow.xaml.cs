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
using System.IO;

namespace MapRxSpatial
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
      //  MovingObjectHandler m;
        SimulationFromText simFromTxt;
        public MainWindow()
        {
            
            InitializeComponent();
            IntersectMgmt.initPolygon();
            simFromTxt = new SimulationFromText(myMap);
          //  m = new MovingObjectHandler(myMap);
        }
        private void Road_Click(object sender, RoutedEventArgs e)    
        {

            simFromTxt.procTraffic();

           // m.procTraffic();
            
        }

        private void button2_Click(object sender, RoutedEventArgs e)        // clear button
        {
            

        }

        private void button3_Click(object sender, RoutedEventArgs e)    //set value botton
        {
       }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
