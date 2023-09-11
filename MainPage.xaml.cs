namespace CALKU;
using CALKU.Services;
using CALKU.Models;
public partial class MainPage : ContentPage
{
    readonly ILoginRepository _loginRepository = new LoginService();
    public MainPage()//loginpage
	{
		InitializeComponent();
	}
    private async void btnLogin_Clicked(object sender, EventArgs e)
    {
        string userName = txtUserName.Text;
        string password = txtPassword.Text;
        if (userName == null || password == null)
        {
            await DisplayAlert("Warning!", "Please input User Name and Password!", "Ok");
            return;
        }
        UserInfo userInfo = await _loginRepository.Login(userName, password);
        if (userInfo != null)
        {
            await Shell.Current.GoToAsync("Home");
        }
        else
        {
            await DisplayAlert("Warning!", "User Name or Password incorrect!", "Ok");
        }
    }
    private async void btnRegister_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("RegisterPage");
    }

}

