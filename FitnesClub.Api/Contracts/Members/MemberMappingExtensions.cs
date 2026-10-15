using FitnesClub.Domain.Entities;

namespace FitnesClub.Api.Contracts.Members;

public static class MemberMappingExtensions
{
    public static MemberDto ToDto(this Member m) => new(
        m.Id,
        m.PassportNumber,
        m.FirstName,
        m.LastName,
        m.Gender,
        m.BirthDate,
        m.PhoneNumber,
        m.MembershipStartDate,
        m.MembershipEndDate
    );

    public static Member ToEntity(this MemberCreateUpdateDto dto) => new()
    {
        Id = Guid.NewGuid(),
        PassportNumber = dto.PassportNumber,
        FirstName = dto.FirstName,
        LastName = dto.LastName,
        Gender = dto.Gender,
        BirthDate = dto.BirthDate,
        PhoneNumber = dto.PhoneNumber,
        MembershipStartDate = dto.MembershipStartDate,
        MembershipEndDate = dto.MembershipEndDate
    };

    public static void UpdateEntity(this MemberCreateUpdateDto dto, Member m)
    {
        m.PassportNumber = dto.PassportNumber;
        m.FirstName = dto.FirstName;
        m.LastName = dto.LastName;
        m.Gender = dto.Gender;
        m.BirthDate = dto.BirthDate;
        m.PhoneNumber = dto.PhoneNumber;
        m.MembershipStartDate = dto.MembershipStartDate;
        m.MembershipEndDate = dto.MembershipEndDate;
    }
}