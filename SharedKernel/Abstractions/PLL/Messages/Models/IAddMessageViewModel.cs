namespace SharedKernel.Abstractions.PLL.Messages.Models
{
	public interface IAddMessageViewModel
	{
		long UserId { get; set; }
		long RoomId { get; set; }
		string Message { get; set; }
	}
}
