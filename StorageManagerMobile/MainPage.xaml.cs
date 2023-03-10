﻿using CommunityToolkit.Maui.Views;
using StorageManagerMobile.ViewModels;
using StorageManagerMobile.Views;
using System.Reflection;

namespace StorageManagerMobile
{
    public partial class MainPage : FlyoutPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
            this.Detail = new NavigationPage(new HomePage());
        }

        public void ChangePage(string label)
        {
            string PageName = ((MainPageViewModel)BindingContext).MenuList.Find(x => x.Label == label).Page;
            string type = Assembly.GetCallingAssembly().GetName().Name + ".Views." + PageName;
            Type PageType = Type.GetType(type);
            Page newPage = ((MainPageViewModel)BindingContext).Pages.Find(x => x.GetType() == PageType);
            this.Detail = new NavigationPage(newPage);
            this.IsPresented = false;
        }

        public void RefreshPage()
        {
            InvalidateMeasure();
            //ChangePage("Home");
            ChangePage("Ingredienti");
            //InvalidateMeasure();
            //this.Detail.ForceLayout();
            //this.Detail.SizeChanged += (s, args) => { };
            //this.ForceLayout();
        }

        private void FlyoutPage_Loaded(object sender, EventArgs e)
        {
            this.IsPresented = true;
        }
    }
}