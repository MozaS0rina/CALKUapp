namespace CALKU;
using CALKU.ViewModels;
using System.ComponentModel;

public partial class CGPA : ContentPage, INotifyPropertyChanged
{
    public readonly SubjectsViewModel _viewModel1;
    public readonly SubjectsViewModel2 _viewModel2;
    public string message;
    public string Message
    {
        get { return message; }
        set { message = value; }
    }
    public CGPA(SubjectsViewModel viewModel1, SubjectsViewModel2 viewModel2)
    {
        InitializeComponent();
        BindingContext = this;
        _viewModel1 = viewModel1;
        _viewModel2 = viewModel2;
    }
    private async void BtnReturnHome(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("Home");
    }
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel1.LoadSubjectsAsync();
        await _viewModel2.LoadSubjectsAsync();
        
    }
    public async void BtnCalcCGPA(object sender, EventArgs e)
    {
        float GPASem1 = await _viewModel1.CalculateGPA();
        float GPASem2 = await _viewModel2.CalculateGPA();
        float CGPA = (GPASem1 + GPASem2) / 2;
        if (CGPA >= 2)
        { 
            Message = "Your CGPA on this year is: " + CGPA + " ! You successfully passed!";
            await DisplayAlert("Congratulations!", Message, "Ok");
        }
        else
        {
            Message = "Your CGPA on this year is: " + CGPA + " ! You failed!";
            await DisplayAlert("Wops!", Message, "Ok");
        }

    }
    public async void BtnCalcGradeForCGPA(object sender, EventArgs e)
    {
        float GPASem1 = await _viewModel1.CalculateGPA();
        float GPASem2 = await _viewModel2.CalculateGPA();
        float CGPA = (GPASem1 + GPASem2) / 2;
        float GradeInPercentage = CGPA / 4 * 100;
        string letterGrade ="";
        if(90<=GradeInPercentage && GradeInPercentage<=100) { letterGrade = "AA"; }
        if (80 <= GradeInPercentage && GradeInPercentage <= 89) { letterGrade = "BA"; }
        if (70 <= GradeInPercentage && GradeInPercentage <= 79) { letterGrade = "BB"; }
        if (65 <= GradeInPercentage && GradeInPercentage <= 69) { letterGrade = "CB"; }
        if (60 <= GradeInPercentage && GradeInPercentage <= 64) { letterGrade = "CC"; }
        if (55 <= GradeInPercentage && GradeInPercentage <= 59) { letterGrade = "DC"; }
        if (50 <= GradeInPercentage && GradeInPercentage <= 54) { letterGrade = "DD"; }
        if (30 <= GradeInPercentage && GradeInPercentage <= 49) { letterGrade = "FD"; }
        if (0 <= GradeInPercentage && GradeInPercentage <= 29) { letterGrade = "FF"; }

        if (CGPA >= 2)
        {
            Message = "Your equivalent grade on this year is: " + GradeInPercentage + " and your letter grade : "+letterGrade+" ! You successfully passed!";
            await DisplayAlert("Congratulations!", Message, "Ok");
        }
        else
        {
            Message = "Your equivalent grade on this year is: " + GradeInPercentage + " and your letter grade : "+letterGrade+" ! You failed!";
            await DisplayAlert("Wops!", Message, "Ok");
        }
    }

}
/*
  public readonly SubjectsViewModel _viewModel1;
    public readonly SubjectsViewModel2 _viewModel2;
    public string message;
    public string Message
    {
        get { return message; }
        set { message = value; }
    }
    public CGPA(SubjectsViewModel viewModel1, SubjectsViewModel2 viewModel2)
	{
        InitializeComponent();
        BindingContext = this;
        _viewModel1 = viewModel1;
        _viewModel2 = viewModel2;
    }
    private async void BtnReturnHome(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("Home");
    }
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel1.LoadSubjectsAsync();
        await _viewModel2.LoadSubjectsAsync();
    }
    public async void BtnCalcCGPA()
    {
        float GPASem1 = await _viewModel1.CalculateGPA();
        float GPASem2 = await _viewModel2.CalculateGPA();
        float CGPA = (GPASem1 + GPASem2) / 2;
        if (CGPA >= 2)
        { Message = "Congratulations!" + "Your cGPA on this year is: " + CGPA + " ! You successfully passed!"; }
        else
        { Message = "Wops!" + "Your CGPA on this year is: " + CGPA + " ! You failed!"; }

    }
//////////////////////////////////////////////
private readonly CGPAViewModel _viewModel;
    public CGPA(CGPAViewModel viewModel)
	{
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
        
    }
    private async void BtnReturnHome(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("Home");
    }
 */