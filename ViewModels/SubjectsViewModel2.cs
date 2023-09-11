using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CALKU.Data;
using CALKU.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace CALKU.ViewModels
{//for the second semester
    public partial class SubjectsViewModel2 : ObservableObject, ISubjectsViewModel
    {
        public readonly DatabaseContextSem2 _context;
        public SubjectsViewModel2()
        {

        }
        public SubjectsViewModel2(DatabaseContextSem2 context)
        {
            _context = context;
        }

        [ObservableProperty]
        private ObservableCollection<Subject> _subjects = new();

        [ObservableProperty]
        private Subject _operatingSubject = new();

        [ObservableProperty]
        private bool _isBusy;

        [ObservableProperty]
        private string _busyText;

        [ObservableProperty]
        public float _GPA = 0;

        public async Task LoadSubjectsAsync()
        {
            await ExecuteAsync(async () =>
            {
                var subjects = await _context.GetAllAsync<Subject>();
                if (subjects is not null && subjects.Any())
                {
                    Subjects ??= new ObservableCollection<Subject>();

                    foreach (var subject in subjects)
                    {
                        Subjects.Add(subject);
                    }
                }
            }, "Fetching subjects...");
        }

        [RelayCommand]
        private void SetOperatingSubject(Subject subject) => OperatingSubject = subject ?? new();


        public async Task<float> CalculateGPA()
        {
            await ExecuteAsync(async () =>
            {
                //Gpa=TotalGradePoints/TotalCredits
                int TotalCredits = 0;  //TotalCredits=Sum(Credits)
                float TotalGradePoints = 0; //TotalGradePoints=Sum(GradePointsForTheSubject)

                float GradePointsForTheSubject = 0;//GradePointsForTheSubject= Credits*Coeficient
                foreach (var subject in Subjects)
                {
                    TotalCredits = TotalCredits + subject.Credits;
                    GradePointsForTheSubject = subject.Coeficient * subject.Credits;
                    TotalGradePoints = TotalGradePoints + GradePointsForTheSubject;

                }

                GPA = TotalGradePoints / TotalCredits;

            }, "Calculating the GPA...");
            return GPA;
        }

        [RelayCommand]
        public async Task SaveSubjectAsync()
        {
            if (OperatingSubject is null)
                return;

            var (isValid, errorMessage) = OperatingSubject.Validate();
            if (!isValid)
            {
                await Shell.Current.DisplayAlert("Validation Error", errorMessage, "Ok");
                return;
            }
            if (isValid) { OperatingSubject.SetTheNoteForGradeAndCoef(); }//set the numeral grade

            var busyText = OperatingSubject.Id == 0 ? "Creating subject..." : "Updating subject...";
            await ExecuteAsync(async () =>
            {
                if (OperatingSubject.Id == 0)
                {
                    // Create Subject
                    await _context.AddItemAsync<Subject>(OperatingSubject);
                    Subjects.Add(OperatingSubject);
                }
                else
                {
                    // Update Subject
                    if (await _context.UpdateItemAsync<Subject>(OperatingSubject))
                    {
                        //using the clone on update
                        var subjectCopy = OperatingSubject.Clone();

                        var index = Subjects.IndexOf(OperatingSubject);
                        Subjects.RemoveAt(index);

                        Subjects.Insert(index, subjectCopy);
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Error", "Subject update error", "Ok");
                        return;
                    }
                }
                SetOperatingSubjectCommand.Execute(new());
            }, busyText);
        }

        [RelayCommand]
        public async Task DeleteSubjectAsync(int id)
        {
            await ExecuteAsync(async () =>
            {
                if (await _context.DeleteItemByKeyAsync<Subject>(id))
                {
                    var subject = Subjects.FirstOrDefault(s => s.Id == id);
                    Subjects.Remove(subject);
                }
                else
                {
                    await Shell.Current.DisplayAlert("Delete Error", "Subject was not deleted", "Ok");
                }
            }, "Deleting subject...");
        }

        private async Task ExecuteAsync(Func<Task> operation, string busyText = null)
        {
            IsBusy = true;
            BusyText = busyText ?? "Processing...";
            try
            {
                await operation?.Invoke();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsBusy = false;
                BusyText = "Processing...";
            }
        }
    }
}
