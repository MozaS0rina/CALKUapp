namespace CALKU;
using CALKU.Services;
using CALKU.Models;
public partial class RegisterPage : ContentPage
{

    readonly IRegisterRepository _registerRepository = new RegisterService();
    public RegisterPage()
    {
        InitializeComponent();
    }

    private async void btnRegister_Clicked(object sender, EventArgs e)
    {
        string userName = txtUserName.Text;
        string password = txtPassword.Text;
        string email = txtEmail.Text;
        if (userName == null || password == null || email == null)
        {
            await DisplayAlert("Warning!", "Please input User Name, Password and Email!", "Ok");
            return;
        }
        UserInfo userInfo = await _registerRepository.Register(userName, password, email);
        if (userInfo != null)
        {
            await Shell.Current.GoToAsync("Home");
        }
        else
        {
            await DisplayAlert("Warning!", "The user already exists!", "Ok");
        }

    }
    private async void btnLogin_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///MainPage");
    }
}