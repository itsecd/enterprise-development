using FitnesClub.Domain.Entities;
using FitnesClub.Domain.Interfaces;
using FitnesClub.Data.Data;

namespace FitnesClub.Data.Repositories;

/// <summary>
/// Репозиторий для хранения и управления данными клиентов фитнес-клуба в оперативной памяти.
/// </summary>
public class InMemoryMemberRepo : IRepository<Member, Guid>
{
    private readonly List<Member> _members = [.. DataSeeder.Members];

    /// <inheritdoc />
    public void Create(Member member)
    {
        if (member.Id == Guid.Empty)
        {
            member.Id = Guid.NewGuid();
        }

        _members.Add(member);
    }

    /// <inheritdoc />
    public Member? Read(Guid id)
    {
        return _members.FirstOrDefault(m => m.Id == id);
    }

    /// <inheritdoc />
    public List<Member> ReadAll()
    {
        return [.. _members];
    }

    /// <inheritdoc />
    public void Update(Member updatedMember)
    {
        var index = _members.FindIndex(m => m.Id == updatedMember.Id);
        if (index != -1)
        {
            _members[index] = updatedMember;
        }
    }

    /// <inheritdoc />
    public void Delete(Guid id)
    {
        _members.RemoveAll(m => m.Id == id);
    }
}