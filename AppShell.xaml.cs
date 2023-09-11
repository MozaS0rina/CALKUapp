namespace CALKU;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
       
        Routing.RegisterRoute("RegisterPage", typeof(RegisterPage));//REGISTER
        Routing.RegisterRoute("Home", typeof(Home));
       
        Routing.RegisterRoute("Subjects", typeof(Subjects));//sem1
        Routing.RegisterRoute("Subjects2", typeof(Subjects2));//sem2
        Routing.RegisterRoute("CGPA", typeof(CGPA));//cumulative gpa

        Routing.RegisterRoute("StudyYearsPage", typeof(StudyYearsPage));
        Routing.RegisterRoute("StudyYearsSubjects", typeof(StudyYearsSubjects));
    }
}
