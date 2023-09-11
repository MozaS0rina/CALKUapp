using CommunityToolkit.Mvvm.Input;

namespace CALKU;

public partial class Home : ContentPage
{
	public Home()
	{
        InitializeComponent();
        BindingContext=this;
	}
    private async void BtnStudyYearsClicked(Object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("StudyYearsPage");
    }
    private async void BtnSubjectsClicked(Object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("Subjects");
    }
    public async void BtnLogout(Object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///MainPage");
    }
    private async void BtnSubjects2Clicked(Object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("Subjects2");
    }
    private async void BtnCGPA_clicked(Object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("CGPA");
    }
}
/*
      <Button
                x:Name="StudyYearsPage"
                Text="Go to Years of Study"
                SemanticProperties.Hint="Study years button"
                Clicked="BtnStudyYearsClicked"
                HorizontalOptions="Center"
                WidthRequest="250"
                HeightRequest="50"
                BorderColor="Black"/>
     */