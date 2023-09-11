using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CALKU.Models
{
    public class Subject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string SubjectName { get; set; }
        public string Grade { get; set; }
        public int Credits { get; set; }
        public float NumeralGrade { get;set; }//from 100 to 0 
        public float Coeficient { get; set; }//Point Value for grade
        public Subject Clone() => MemberwiseClone() as Subject;
        public (bool IsValid, string ErrorMessage) Validate()
        {
            if (string.IsNullOrWhiteSpace(SubjectName))
            {
                return (false, "Subject's name is required.");
            }
            else if (Credits <= 0)
            {
                return (false, "The number of credits should be greater than 0.");
            }
            else if (Grade == "")
            {
                return (false, "Subject's grade is required.");
            }
            else if (Grade != "AA" && Grade != "BA" && Grade != "BB" &&
                Grade != "CB" && Grade != "CC" && Grade != "DC" &&
                Grade != "DD" && Grade != "FD" && Grade != "FF" &&
                Grade != "DZ")
            {
                return (false, "Incorrect grade! Grade should be: AA, BA, BB, CB, CC, DC, DD, FD, FF or DZ!");
            }
            return (true, null);

        }
        public void SetTheNoteForGradeAndCoef()
        {

            if (Grade == "AA")
            { NumeralGrade = 100; Coeficient = (float)4; }
            if (Grade == "BA")
            { NumeralGrade = 89; Coeficient = (float)3.5; }
            if (Grade == "BB")
            { NumeralGrade = 79; Coeficient = (float)3; }
            if (Grade == "CB")
            { NumeralGrade = 69;Coeficient = (float)2.5; }
            if (Grade == "CC")
            { NumeralGrade = 64; Coeficient = (float)2; }
            if (Grade == "DC")
            { NumeralGrade = 59;  Coeficient = (float)1.5; }
            if (Grade == "DD")
            { NumeralGrade = 54; Coeficient = (float)1; }
            if (Grade == "FD")
            { NumeralGrade = 49; Coeficient = (float)0.5; }
            if (Grade == "FF")
            { NumeralGrade = 29; Coeficient = (float)0; }
            if (Grade == "DZ")
            { NumeralGrade = 0; Coeficient = (float)0; }

            
        }
    }
}
