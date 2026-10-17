using FitnesClub.Domain.Data;
using FitnesClub.Domain.Entities;

namespace FitnesClub.Api.Storage;

/// <summary>
/// Репозиторий для хранения и управления данными клиентов фитнес-клуба в оперативной памяти.
/// </summary>
public class InMemoryMemberRepo : IRepository<Member, Guid>
{
    private readonly List<Member> _members;

    /// <summary>
    /// Инициализирует репозиторий клиентов начальными данными из <see cref="DataSeeder"/>.
    /// </summary>
    public InMemoryMemberRepo()
    {
        _members = [.. DataSeeder.Members];
    }

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
        var item = Read(id);
        if (item is not null)
        {
            _members.Remove(item);
        }
    }
}