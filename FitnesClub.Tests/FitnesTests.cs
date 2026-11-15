namespace FitnesClub.Tests;

public class FitnesTests(FitnesFixtures fixture) : IClassFixture<FitnesFixtures>
{
    private static readonly DateTime _date = new(2026, 9, 28, 12, 0, 0);

    /// <summary>
    /// 1. Вывести информацию о всех тренерах, стаж работы которых не менее 5 лет
    /// </summary>
    [Fact]
    public void Trainers_WithExperienceAtLeastFiveYears_ShouldBeReturned()
    {
        var expected = new[]
        {
            fixture.Trainers[0],
            fixture.Trainers[1],
            fixture.Trainers[2],
            fixture.Trainers[4],
            fixture.Trainers[6],
            fixture.Trainers[7],
            fixture.Trainers[8]
        };

        var actual = fixture.Trainers
            .Where(trainer => trainer.WorkExperienceYears >= 5)
            .ToList();

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// 2. Проверить доступность зала: проверяем оба граничных исхода на реальных данных
    /// </summary>
    [Fact]
    public void Room_ShouldBeAvailable_WhenThereIsNoCurrentBooking()
    {
        var roomName = "Зал №1";

        var isRoomAvailable = !fixture.Bookings.Any(booking =>
            booking.RoomName == roomName &&
            booking.LessonDateTime <= _date &&
            booking.LessonDateTime.Add(booking.Duration) > _date);

        Assert.True(isRoomAvailable);
    }

    [Fact]
    public void Room_ShouldNotBeAvailable_WhenBookingIsInProgress()
    {
        var busyBooking = fixture.Bookings[0];

        var busyTime = busyBooking.LessonDateTime.AddMinutes(busyBooking.Duration.TotalMinutes / 2);

        var isRoomAvailable = !fixture.Bookings.Any(booking =>
            booking.RoomName == busyBooking.RoomName &&
            booking.LessonDateTime <= busyTime &&
            booking.LessonDateTime.Add(booking.Duration) > busyTime);

        Assert.False(isRoomAvailable);
    }

    /// <summary>
    /// 3. Вывести информацию о клиентах, у которых просрочен абонемент, упорядочить по ФИО
    /// </summary>
    [Fact]
    public void ExpiredMemberships_ShouldBeSortedByFullName()
    {
        var expected = new[]
        {
            fixture.Members[6],
            fixture.Members[4],
            fixture.Members[8],
            fixture.Members[2]
        };

        var actual = fixture.Members
            .Where(member => member.MembershipEndDate < _date)
            .OrderBy(member => member.LastName)
            .ThenBy(member => member.FirstName)
            .ToList();

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// 4. Вывести информацию о занятиях за текущий месяц, проходящих в выбранном зале
    /// </summary>
    [Fact]
    public void Bookings_ShouldBeReturnedForCurrentMonthAndSelectedRoom()
    {
        var roomName = "Зал №1";

        var expected = new[]
        {
            fixture.Bookings[0],
            fixture.Bookings[2]
        };

        var actual = fixture.Bookings
            .Where(booking =>
                booking.RoomName == roomName &&
                booking.LessonDateTime.Year == _date.Year &&
                booking.LessonDateTime.Month == _date.Month)
            .ToList();

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// 5. Вывести топ 5 наиболее популярных тренеров
    /// </summary>
    [Fact]
    public void TopFiveMostPopularTrainers_ShouldBeReturned()
    {
        var expected = new[]
        {
            fixture.Trainers[0],
            fixture.Trainers[2],
            fixture.Trainers[4],
            fixture.Trainers[1],
            fixture.Trainers[3]
        };

        var bookingCounts = fixture.Bookings
            .GroupBy(b => b.TrainerId)
            .ToDictionary(g => g.Key, g => g.Count());

        var actual = fixture.Trainers
            .Select(trainer => new
            {
                Trainer = trainer,
                BookingCount = bookingCounts.GetValueOrDefault(trainer.Id, 0)
            })
            .OrderByDescending(x => x.BookingCount)
            .ThenBy(x => x.Trainer.LastName)
            .Take(5)
            .Select(x => x.Trainer)
            .ToList();

        Assert.Equal(expected, actual);
    }
}