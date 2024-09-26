using InstitutionStatistic.Query.Enums;
using InstitutionStatistic.Query.Models;
using System.Xml.Linq;

namespace InstitutionStatistic.Query.Test;

/// <summary>
/// Базовый класс для тестов
/// </summary>
public class TestBase
{
    protected List<Institution> Institutions = new List<Institution>();

    protected List<Speciality> Specialities = new List<Speciality>();

    /// <summary>
    /// ctor
    /// </summary>
    public TestBase()
    {

        #region Rectors
        var rector1 = new Rector(ScientificDegree.Candidate, AcademicRank.Docent) { Id = Guid.NewGuid(), Version = DateTime.Now, FullName = "Иванов A.Ю" };
        var rector2 = new Rector(ScientificDegree.Doctor, AcademicRank.Professor) { Id = Guid.NewGuid(), Version = DateTime.Now, FullName = "Петров В.А" };
        var rector3 = new Rector(ScientificDegree.Candidate, AcademicRank.Docent) { Id = Guid.NewGuid(), Version = DateTime.Now, FullName = "Сидоров Н.П" };
        var rector4 = new Rector(ScientificDegree.Doctor, AcademicRank.Professor) { Id = Guid.NewGuid(), Version = DateTime.Now, FullName = "Бобров А.М" };
        #endregion

        #region Faculties
        var faclt1 = new Faculty() { Id = Guid.NewGuid(), Version = DateTime.Now, Name = "FC1" };
        var faclt2 = new Faculty() { Id = Guid.NewGuid(), Version = DateTime.Now, Name = "FC2" };
        var faclt3 = new Faculty() { Id = Guid.NewGuid(), Version = DateTime.Now, Name = "FC3" };
        var faclt4 = new Faculty() { Id = Guid.NewGuid(), Version = DateTime.Now, Name = "FC4" };
        var faclt5 = new Faculty() { Id = Guid.NewGuid(), Version = DateTime.Now, Name = "FC5" };
        #endregion

        #region Departments
        var department1 = new Department() { Name = "ГИИБ", Id = Guid.NewGuid(), Version = DateTime.Now };
        var department2 = new Department() { Name = "ИСТ", Id = Guid.NewGuid(), Version = DateTime.Now };
        var department3 = new Department() { Name = "ЛБС", Id = Guid.NewGuid(), Version = DateTime.Now };
        var department4 = new Department() {Name = "TEST1", Id = Guid.NewGuid(), Version = DateTime.Now };
        var department5 = new Department() {Name = "TEST2", Id = Guid.NewGuid(), Version = DateTime.Now };
        var department6 = new Department() {Name = "TEST3", Id = Guid.NewGuid(), Version = DateTime.Now };
        #endregion

        #region Specialities
        var speciality1 = new Speciality() { Name = "SPEC1", Code = "123456", Id = Guid.NewGuid(), Version = DateTime.Now };
        var speciality2 = new Speciality() { Name = "SPEC2", Code = "234567", Id = Guid.NewGuid(), Version = DateTime.Now };
        var speciality3 = new Speciality() { Name = "SPEC3", Code = "345678", Id = Guid.NewGuid(), Version = DateTime.Now };
        var speciality4 = new Speciality() { Name = "SPEC4", Code = "456789", Id = Guid.NewGuid(), Version = DateTime.Now };
        var speciality5 = new Speciality() { Name = "SPEC5", Code = "567890", Id = Guid.NewGuid(), Version = DateTime.Now };
        var speciality6 = new Speciality() { Name = "SPEC6", Code = "678901", Id = Guid.NewGuid(), Version = DateTime.Now };
        #endregion

        #region Groups
        var group1 = new Group(department1, speciality1) { Id = Guid.NewGuid(), Version = DateTime.Now, Number = "6412-100503" };
        var group2 = new Group(department2, speciality2) { Id = Guid.NewGuid(), Version = DateTime.Now, Number = "6411-100503" };
        var group3 = new Group(department2, speciality3) { Id = Guid.NewGuid(), Version = DateTime.Now, Number = "6413-100503" };


        var group4 = new Group(department3, speciality1) { Id = Guid.NewGuid(), Version = DateTime.Now, Number = "6414-100503" };
        var group5 = new Group(department3, speciality2) { Id = Guid.NewGuid(), Version = DateTime.Now, Number = "5101-100503" };
        var group6 = new Group(department3, speciality3) { Id = Guid.NewGuid(), Version = DateTime.Now, Number = "5102-100503" };
        var group7 = new Group(department3, speciality4) { Id = Guid.NewGuid(), Version = DateTime.Now, Number = "5103-100503" };
        var group8 = new Group(department3, speciality1) { Id = Guid.NewGuid(), Version = DateTime.Now, Number = "5104-100503" };

        var group9 = new Group(department4, speciality1) { Id = Guid.NewGuid(), Version = DateTime.Now, Number = "3221-100503" };
        var group10 = new Group(department4, speciality2) { Id = Guid.NewGuid(), Version = DateTime.Now, Number = "3222-100503" };
        var group11 = new Group(department4, speciality3) { Id = Guid.NewGuid(), Version = DateTime.Now, Number = "3223-100503" };
        var group12 = new Group(department5, speciality4) { Id = Guid.NewGuid(), Version = DateTime.Now, Number = "3223-100503" };
        var group13 = new Group(department5, speciality4) { Id = Guid.NewGuid(), Version = DateTime.Now, Number = "7405-100503" };

        var group14 = new Group(department6, speciality3) { Id = Guid.NewGuid(), Version = DateTime.Now, Number = "7406-100503" };
        var group15 = new Group(department6, speciality2) { Id = Guid.NewGuid(), Version = DateTime.Now, Number = "7407-100503" };
        var group16 = new Group(department6, speciality4) { Id = Guid.NewGuid(), Version = DateTime.Now, Number = "7408-100503" };

        var group17 = new Group(department6, speciality5) { Id = Guid.NewGuid(), Version = DateTime.Now, Number = "7409-100503" };
        #endregion

        #region Institutions
        var inst1 = new Institution(
            rector1,
            BuildingOwnership.Federal,
            InstitutionOwnership.Municipality)
        { Id = Guid.NewGuid(), Version = DateTime.Now, Name = "СГАУ", RegistrationNumber = "test register1", Address = "test address1" };

        var inst2 = new Institution(
            rector2,
            BuildingOwnership.Municipality,
            InstitutionOwnership.Municipality)
        { Id = Guid.NewGuid(), Version = DateTime.Now, Name = "САМГТУ", RegistrationNumber = "test register2", Address = "test address2" };

        var inst3 = new Institution(
            rector3,
            BuildingOwnership.Personal,
            InstitutionOwnership.Personal)
        { Id = Guid.NewGuid(), Version = DateTime.Now, Name = "ПГУТИ", RegistrationNumber = "test register3", Address = "test address3" };

        var inst4 = new Institution(
            rector4,
            BuildingOwnership.Federal,
            InstitutionOwnership.Personal)
        { Id = Guid.NewGuid(), Version = DateTime.Now, Name = "САМГМУ", RegistrationNumber = "test register4", Address = "test address4" };
        #endregion

        #region initAllValues
        //первый институт
        inst1.Faculties.Add(faclt1);
        inst1.Faculties.Add(faclt2);

        faclt1.Institution = inst1;
        faclt2.Institution = inst1;

        faclt1.Departments.Add(department1);
        faclt2.Departments.Add(department2);

        department1.Faculty = faclt1;
        department2.Faculty = faclt2;

        department1.Groups.Add(group1);
        department2.Groups.Add(group2);
        department2.Groups.Add(group3);


        speciality1.Groups.Add(group1);
        speciality2.Groups.Add(group2);
        speciality3.Groups.Add(group3);

        //воторой институт
        inst2.Faculties.Add(faclt3);

        faclt3.Institution = inst2;

        faclt3.Departments.Add(department3);

        department3.Faculty = faclt3;

        department3.Groups.Add(group4);
        department3.Groups.Add(group5);
        department3.Groups.Add(group6);
        department3.Groups.Add(group7);
        department3.Groups.Add(group8);

        speciality1.Groups.Add(group4);
        speciality1.Groups.Add(group8);
        speciality2.Groups.Add(group5);
        speciality4.Groups.Add(group7);
        speciality3.Groups.Add(group6);

        //третий институт
        inst3.Faculties.Add(faclt4);

        faclt4.Institution = inst3;

        faclt4.Departments.Add(department4);
        faclt4.Departments.Add(department5);

        department4.Faculty = faclt4;
        department5.Faculty = faclt4;

        department4.Groups.Add(group9);
        department4.Groups.Add(group10);
        department4.Groups.Add(group11);
        department5.Groups.Add(group12);
        department5.Groups.Add(group13);

        speciality1.Groups.Add(group9);
        speciality2.Groups.Add(group10);
        speciality3.Groups.Add(group11);
        speciality4.Groups.Add(group12);
        speciality4.Groups.Add(group13);

        //четвертый институт
        inst4.Faculties.Add(faclt5);

        faclt5.Institution = inst4;

        faclt5.Departments.Add(department6);

        department6.Faculty = faclt4;

        department6.Groups.Add(group14);
        department6.Groups.Add(group15);
        department6.Groups.Add(group16);
        department6.Groups.Add(group17);

        speciality3.Groups.Add(group14);
        speciality2.Groups.Add(group15);
        speciality4.Groups.Add(group16);
        speciality5.Groups.Add(group17);
        //Добавляем институты и специальности...
        Institutions.Add(inst1);
        Institutions.Add(inst2);
        Institutions.Add(inst3);
        Institutions.Add(inst4);
        Specialities.Add(speciality1);
        Specialities.Add(speciality2);
        Specialities.Add(speciality3);
        Specialities.Add(speciality4);
        Specialities.Add(speciality5);
        Specialities.Add(speciality6);
        #endregion
    }
}
