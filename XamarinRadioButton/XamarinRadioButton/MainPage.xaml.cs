using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace XamarinRadioButton
{

    public partial class MainPage : ContentPage
    {
        private List<RadioButtonControl> radioBtnCtrlList;
        private const string radioBtnGroupName = "transactionRadioBtn";
        public MainPage()
        {
            InitializeComponent();
            radioBtnCtrlList = new List<RadioButtonControl>();

            var dataList = getDataMOdels();

            listView.ItemsSource = dataList;
        }

        private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {           
            radioBtnCtrlList[e.SelectedItemIndex].RadioBtnTapHandler(null, null);
        }

        private void OnCheckedChanged(object sender, EventArgs e)
        {
            var selectedRad = sender as RadioButtonControl;

            foreach (var rad in radioBtnCtrlList)
            {
                if (!selectedRad.Uid.Equals(rad.Uid))
                {
                    rad.Checked = false;
                }
                else
                {
                    rad.Checked = true;
                }
            }
        }

        private void transactionList_ChildAdded(object sender, ElementEventArgs e)
        {
            RadioButtonControl radioBtnCtrl = null;

            if (e.Element.ClassId == radioBtnGroupName)
            {
                radioBtnCtrl = e.Element as RadioButtonControl;                
                radioBtnCtrl.CheckedChanged = OnCheckedChanged;
                radioBtnCtrlList.Add(radioBtnCtrl);
            }

        }

        private List<DataModel> getDataMOdels()
        {
            return new List<DataModel>()
            {
               new DataModel() {UniqNo = 1, Name = "shubh", Amount=111, Date="01/10/2020", Description= "Amazon"},
               new DataModel() {UniqNo = 2, Name = "s", Amount=10, Date="01/10/2020", Description= "Google"},
               new DataModel() {UniqNo = 3, Name = "j", Amount=120, Date="01/10/2020", Description= "FB"},
               new DataModel() {UniqNo = 4, Name = "sj", Amount=1200, Date="01/10/2020", Description= "Netflix"},
               new DataModel() {UniqNo = 5, Name = "SJ", Amount=1, Date="01/10/2020", Description= "Adobe"}

            };
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
             radioBtnCtrlList.Select(r => r.BorderColor = Color.Red).ToList();
            //foreach(var rad in radioBtnCtrlList)
            //{
            //    rad.BorderColor = Color.Red;
            //}
        }        
    }

    public class DataModel
    {
        public int UniqNo { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public int Amount { get; set; }
    }
}
