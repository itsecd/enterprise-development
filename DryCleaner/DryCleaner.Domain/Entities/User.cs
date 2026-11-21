using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DryCleaner.Domain.Entities;

/// <summary>
/// Пользователь системы.
/// </summary>
[Table("user")]
public class User
{
    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public string Username { get; set; } = null!;

    /// <summary>
    /// Хэш пароля.
    /// </summary>
    public string PasswordHash { get; set; } = null!;
}

