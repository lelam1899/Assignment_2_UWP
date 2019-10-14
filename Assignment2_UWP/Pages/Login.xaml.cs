using Assignment2_UWP.Entity;
using Assignment2_UWP.Service;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Assignment2_UWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        private MemberService memberService;
        private MemberLogin memberLogin;

        public Login()
        {
            this.InitializeComponent();
            this.memberService = new MemberServiceImp();
        }



        private async void Button_Login_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {


            memberService.Login(Email.Text, Password.Password);

            if (memberService.Login(Email.Text, Password.Password) == null)
            {
                var message = new MessageDialog("Login failed!");
                await message.ShowAsync();
            }
            else
            {
                var message = new MessageDialog("Login success");
                await message.ShowAsync();
            }




        }


        private void Button_Reset_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ResetLoginForm();

        }
        private void ResetLoginForm()

        {
            this.Email.Text = string.Empty;

            this.Password.Password = string.Empty;
        }
    }
}
