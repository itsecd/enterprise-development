using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VetClinic.Infrastructure.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Breeds",
                columns: new[] { "Id", "Name", "Species" },
                values: new object[,]
                {
                    { 1, "Лабрадор", "Dog" },
                    { 2, "Пудель", "Dog" },
                    { 3, "Хаски", "Dog" },
                    { 4, "Персидская", "Cat" },
                    { 5, "Британская", "Cat" },
                    { 6, "Волнистый попугай", "Bird" },
                    { 7, "Канарейка", "Bird" },
                    { 8, "Рекс", "Rabbit" },
                    { 9, "Карликовый", "Rabbit" },
                    { 10, "Сирийский", "Hamster" },
                    { 11, "Такса", "Dog" }
                });

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "Id", "Address", "FullName", "Phone" },
                values: new object[,]
                {
                    { 1, "ул. Ленина, 1", "Иванов Иван Иванович", "+79001110001" },
                    { 2, "пр. Мира, 5", "Петров Петр Петрович", "+79001110002" },
                    { 3, "ул. Садовая, 12", "Сидорова Анна Викторовна", "+79001110003" },
                    { 4, "ул. Заречная, 3", "Королев Дмитрий Сергеевич", "+79001110004" },
                    { 5, "пер. Тихий, 7", "Никитина Ольга Юрьевна", "+79001110005" },
                    { 6, "ул. Победы, 44", "Федоров Алексей Николаевич", "+79001110006" },
                    { 7, "ул. Центральная, 2", "Морозова Елена Павловна", "+79001110007" },
                    { 8, "ул. Северная, 9", "Зайцев Михаил Олегович", "+79001110008" },
                    { 9, "ул. Восточная, 18", "Соколова Марина Ивановна", "+79001110009" },
                    { 10, "ул. Лесная, 31", "Волков Сергей Андреевич", "+79001110010" }
                });

            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "Id", "FocusSpecies", "Name" },
                values: new object[,]
                {
                    { 1, "Dog", "Хирург (собаки)" },
                    { 2, "Cat", "Терапевт (кошки)" },
                    { 3, "Bird", "Орнитолог" },
                    { 4, "Rabbit", "Экзотика (кролики)" },
                    { 5, null, "Терапевт (общий)" },
                    { 6, "Dog", "Кардиолог (собаки)" },
                    { 7, "Cat", "Дерматолог (кошки)" },
                    { 8, "Hamster", "Экзотика (хомяки)" },
                    { 9, null, "Хирург (общий)" },
                    { 10, "Dog", "Невролог (собаки)" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "PasswordHash", "Username" },
                values: new object[] { 1, "AQAAAAIAAYagAAAAEKdgA90st4O6skWJJR/QM+V9mf+uHaN30zJ/xJ1ni1C6w/497fhpJc+dFpELkXldww==", "admin" });

            migrationBuilder.InsertData(
                table: "Pets",
                columns: new[] { "Id", "BirthDate", "BreedId", "Nickname", "OwnerId", "Species", "Weight" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Барс", 1, "Cat", 4.5 },
                    { 2, new DateTime(2020, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Мурка", 2, "Cat", 3.7999999999999998 },
                    { 3, new DateTime(2018, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Рекс", 3, "Dog", 28.0 },
                    { 4, new DateTime(2017, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Буян", 4, "Dog", 23.5 },
                    { 5, new DateTime(2021, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, "Снежок", 5, "Rabbit", 1.8 },
                    { 6, new DateTime(2020, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "Кеша", 6, "Bird", 0.040000000000000001 },
                    { 7, new DateTime(2022, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, "Белка", 7, "Hamster", 0.14999999999999999 },
                    { 8, new DateTime(2021, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, "Пушок", 8, "Rabbit", 1.2 },
                    { 9, new DateTime(2019, 8, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Тоби", 1, "Dog", 7.0 },
                    { 10, new DateTime(2020, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Лаки", 3, "Dog", 25.5 },
                    { 11, new DateTime(2018, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, "Жучка", 2, "Dog", 9.5 }
                });

            migrationBuilder.InsertData(
                table: "Vets",
                columns: new[] { "Id", "BirthYear", "FullName", "PassportNumber", "SpecializationId", "WorkExperienceYears" },
                values: new object[,]
                {
                    { 1, 1980, "Смирнов Антон Валерьевич", "4501 000001", 1, 15 },
                    { 2, 1975, "Кузнецов Игорь Петрович", "4501 000002", 2, 22 },
                    { 3, 1988, "Попова Наталья Сергеевна", "4501 000003", 3, 8 },
                    { 4, 1983, "Новиков Роман Михайлович", "4501 000004", 4, 12 },
                    { 5, 1990, "Лебедев Павел Денисович", "4501 000005", 5, 6 },
                    { 6, 1978, "Королева Светлана Алексеевна", "4501 000006", 6, 18 },
                    { 7, 1985, "Михайлов Денис Юрьевич", "4501 000007", 7, 10 },
                    { 8, 1992, "Горбунов Кирилл Олегович", "4501 000008", 8, 4 },
                    { 9, 1981, "Титова Ирина Владимировна", "4501 000009", 9, 14 },
                    { 10, 1970, "Сорокин Владимир Федорович", "4501 000010", 10, 28 }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "DateTime", "IsRepeat", "PetId", "RoomNumber", "VetId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 3, 10, 0, 0, 0, DateTimeKind.Unspecified), true, 3, "101", 1 },
                    { 2, new DateTime(2024, 6, 5, 11, 30, 0, 0, DateTimeKind.Unspecified), true, 4, "101", 1 },
                    { 3, new DateTime(2024, 6, 10, 9, 0, 0, 0, DateTimeKind.Unspecified), true, 10, "101", 6 },
                    { 4, new DateTime(2024, 6, 12, 14, 0, 0, 0, DateTimeKind.Unspecified), false, 1, "205", 2 },
                    { 5, new DateTime(2024, 6, 18, 16, 30, 0, 0, DateTimeKind.Unspecified), true, 2, "205", 7 },
                    { 6, new DateTime(2024, 6, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), false, 6, "303", 3 },
                    { 7, new DateTime(2024, 6, 25, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 5, "303", 4 },
                    { 8, new DateTime(2024, 5, 14, 9, 0, 0, 0, DateTimeKind.Unspecified), true, 3, "101", 1 },
                    { 9, new DateTime(2024, 5, 20, 11, 0, 0, 0, DateTimeKind.Unspecified), false, 4, "101", 6 },
                    { 10, new DateTime(2024, 4, 5, 10, 0, 0, 0, DateTimeKind.Unspecified), false, 3, "205", 1 },
                    { 11, new DateTime(2024, 4, 7, 15, 0, 0, 0, DateTimeKind.Unspecified), false, 10, "205", 6 },
                    { 12, new DateTime(2024, 3, 1, 9, 30, 0, 0, DateTimeKind.Unspecified), false, 9, "101", 1 },
                    { 13, new DateTime(2024, 6, 7, 14, 0, 0, 0, DateTimeKind.Unspecified), false, 11, "101", 1 },
                    { 14, new DateTime(2024, 6, 22, 10, 0, 0, 0, DateTimeKind.Unspecified), true, 7, "404", 8 },
                    { 15, new DateTime(2024, 6, 28, 11, 0, 0, 0, DateTimeKind.Unspecified), false, 8, "404", 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Vets",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Vets",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Vets",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Vets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Vets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Vets",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Vets",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Vets",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Vets",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Vets",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
