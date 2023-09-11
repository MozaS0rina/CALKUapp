namespace CALKU;
using CALKU.ViewModels;
public partial class Subjects2 : ContentPage
{
    private readonly SubjectsViewModel2 _viewModel;
    public Subjects2(SubjectsViewModel2 viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadSubjectsAsync();
    }
    private async void BtnCalculateGPA(Object sender, EventArgs e)
    {
        float GPA = await _viewModel.CalculateGPA();
        if (GPA > 1.8)
        {
            await DisplayAlert("Congratulations!", "Your GPA on second semester is: " + GPA + " ! You have a good GPA and successfully passed!", "Ok");
        }
        else
        {
            await DisplayAlert("Wops!", "Your GPA is: " + GPA + " ! Watch your GPA or you will fail!", "Ok");
        }
    }
}