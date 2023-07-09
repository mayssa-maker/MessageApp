using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MessageAppBack.Models{
public class Conversation

    {
   
    [Key]
    public int ConversationId { get; set; }
    
    public int User1Id { get; set; }
    public UserModel User1 { get; set; }

    public int User2Id { get; set; }
    public UserModel User2 { get; set; }

    public List<MessageModel> Messages { get; set; }

  

    public Conversation()
    {
        Messages = new List<MessageModel>();
    }
}


}