using System.ComponentModel.DataAnnotations.Schema;


namespace GoedBezigWebApp.Models
{
    public class ActivityTaskUser
    {
        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public User User { get; set; }
        [ForeignKey("ActivityTaskId")]
        public int ActivityTaskId { get; set; }
        public ActivityTask ActivityTask { get; set; }

        public ActivityTaskUser()
        {
        }

        public ActivityTaskUser(User user, ActivityTask activityTask)
        {
            
            User = user;
            UserId = user.Id;
            ActivityTask = activityTask;
            ActivityTaskId = activityTask.Id;
        }
    }
}
