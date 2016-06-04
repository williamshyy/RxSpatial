using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Bing.Maps;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MapDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        MapLayer mMapLayer;
        public MainPage()
        {
  
            this.InitializeComponent();
            mainMap.SetView(new Location(47.5319677333823, -122.2016143798835), 12);
            mMapLayer = new MapLayer();
            mainMap.Children.Add(mMapLayer);

        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        Pushpin[] pushpin = new Pushpin[5];
        private void Road_Click(object sender, RoutedEventArgs e)
        {
            //Issue1, file
            //Issue2, thread
            string[] traffic=getData();
            foreach (string line in traffic)
            {
                //try
               // {
              //  Task.Delay(1000);
                      //  Thread.Sleep(1000);
               // System.Windows.Forms.Timer();
                
                    string[] strs = line.Split(' ');
                    if (strs[2].Equals("newpoint"))
                    {
                        int pushpinId = Int32.Parse(strs[0].Trim());
                        pushpin[pushpinId] = new Pushpin() { Text = strs[0].Trim() };
                        MapLayer.SetPosition(pushpin[pushpinId], new Location(Convert.ToDouble(strs[3].Trim()), Convert.ToDouble(strs[4].Trim())));
                        mMapLayer.Children.Add(pushpin[pushpinId]);
                    }
                    if (strs[2].Equals("point"))
                    {
                        int pushpinId = Int32.Parse(strs[0].Trim());
                        MapLayer.SetPosition(pushpin[pushpinId], new Location(Convert.ToDouble(strs[3].Trim()), Convert.ToDouble(strs[4].Trim())));
                    }
                    MapLayer.SetPosition(pushpin[0],new Location(47,122));
                //}
                //catch(Exception exception)
                //{

                //}
            }
        }

        private string[] getData()
        {

          //   FileStream file = new FileStream("E:\\test.txt", FileMode.Open);
          //  StreamReader sr = new StreamReader(@"c:\trafficData\trafficData.txt", Encoding.UTF8);
            //should discard beginning 2 lines read line by line
            TextReader tr = new StreamReader(@"c:\trafficData\trafficData.txt");

            string[] traffic = new string[20];
            traffic[0] = "0 0 newpoint 47.5498695 -122.2509011";
            traffic[1] = "1 0 newpoint 47.4817583 -122.2891443";
            traffic[2] = "2 0 newpoint 47.5615934 -122.3077714";
            traffic[3] = "3 0 newpoint 47.5677095 -122.0988198";
            traffic[4] = "4 0 newpoint 47.5618881 -122.1395184";
            traffic[5] = "0 1 point 47.5502122 -122.2499888";
            traffic[6] = "1 1 point 47.48198 -122.2891909";
            traffic[7] = "2 1 point 47.5615387 -122.3075997";
            traffic[8] = "3 1 point 47.5676271 -122.09871";
            traffic[9] = "4 1 point 47.5620509 -122.139438";
            traffic[10] = "0 2 point 47.550484 -122.2495327";
            traffic[11] = "1 2 point 47.4822386 -122.2892127";
            traffic[12] = "2 2 point 47.5612827 -122.3071891";
            traffic[13] = "3 2 point 47.5672738 -122.0984868";
            traffic[14] = "4 2 point 47.5623246 -122.139213";
            traffic[15] = "0 3 point 47.5506779 -122.2493678";
            traffic[16] = "1 3 point 47.482452 -122.2892103";
            traffic[17] = "2 3 point 47.561203 -122.307074";
            traffic[18] = "3 3 point 47.5668858 -122.0983237";
            traffic[19] = "4 3 point 47.5622807 -122.1389957";
            return traffic;
        }
    }
}
