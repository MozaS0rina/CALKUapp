namespace CALKU;
using CALKU.ViewModels;


public partial class Subjects : ContentPage
{
    private readonly SubjectsViewModel _viewModel;
    public Subjects(SubjectsViewModel viewModel)
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
            await DisplayAlert("Congratulations!", "Your GPA on first semester is: " + GPA + " ! You have a good GPA and successfully passed!", "Ok");
        }
        else
        {
            await DisplayAlert("Wops!", "Your GPA is: " + GPA + " ! Watch your GPA or you will fail!", "Ok");
        }
    }

}