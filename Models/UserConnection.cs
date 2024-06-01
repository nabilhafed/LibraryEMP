using System;
using System.Collections.Generic;

namespace LibraryEMP.Models;

public partial class UserConnection
{
	public string Username { get; set; } = null!;
	public string? Password { get; set; }
	public string? Type { get; set; }
	public string? SessionID { get; set; }
	public DateTime? SessionExpires { get; set; }
}

