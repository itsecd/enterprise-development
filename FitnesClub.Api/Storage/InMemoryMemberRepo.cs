using FitnesClub.Domain.Data;
using FitnesClub.Domain.Entities;

namespace FitnesClub.Api.Storage;

public class InMemoryMemberRepo : IRepository<Member, Guid>
{
    private readonly List<Member> _members;

    public InMemoryMemberRepo()
    {
        _members = [.. DataSeeder.Members];
    }

    public void Create(Member member)
    {
        if (member.Id == Guid.Empty)
        {
            member.Id = Guid.NewGuid();
        }

        _members.Add(member);
    }

    public Member? Read(Guid id)
    {
        return _members.FirstOrDefault(m => m.Id == id);
    }

    public List<Member> ReadAll()
    {
        return [.. _members];
    }

    public void Update(Member updatedMember)
    {
        var index = _members.FindIndex(m => m.Id == updatedMember.Id);
        if (index != -1)
        {
            _members[index] = updatedMember;
        }
    }

    public void Delete(Guid id)
    {
        var item = Read(id);
        if (item is not null)
        {
            _members.Remove(item);
        }
    }
}