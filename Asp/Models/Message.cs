using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Models
{
    public class Message
    {
        [Required]
        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { set; get; }

        [Required]
        public string Text { set; get; }

        [Required]
        public DateTime Date { set; get; }

        [Required]
        public string Receiver { set; get; }

        [Required]
        public string Sender { set; get; }
    }

    //public class Message : Hub
    //{
    //    [Required]
    //    [Key]
    //    [ScaffoldColumn(false)]
    //    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    public string Id { set; get; } = Guid.NewGuid().ToString();

    //    [Required]
    //    public string Text { set; get; }

    //    [Required]
    //    public DateTime dateAdded { set; get; }

    //    [Required]
    //    public string Sender { set; get; }

    //    [Required]
    //    public int GroupId { get; set; }

    //    public Task SendMessage(string user, string message)
    //    {
    //        return Clients.All.SendAsync("ReceiveMessage", user, message);
    //    }

    //    public Task SendMessageToCaller(string user, string message)
    //    {
    //        return Clients.Caller.SendAsync("ReceiveMessage", user, message);
    //    }

    //    public Task SendMessageToGroup(string user, string message)
    //    {
    //        return Clients.Group("Group Users").SendAsync("ReceiveMessage", user, message);
    //    }
    //}

    //public class UserGroup
    //{
    //    [Required]
    //    [Key]
    //    [ScaffoldColumn(false)]
    //    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    public string Id { get; set; } = Guid.NewGuid().ToString();

    //    [Required]
    //    public string UserId { get; set; }

    //    [Required]
    //    public int GroupId { get; set; }
    //}

    //public class Group
    //{
    //    [Required]
    //    [Key]
    //    [ScaffoldColumn(false)]
    //    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    public string Id { get; set; } = Guid.NewGuid().ToString();

    //    [Required]
    //    public string GroupName { get; set; }
    //}
}
