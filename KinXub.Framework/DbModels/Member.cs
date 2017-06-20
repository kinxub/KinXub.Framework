using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KinXub.Models 
{
	public class Member                            
	{
        [DisplayName("ID")]
        public int id { get; set; }
        [DisplayName("名稱")]
        [Required(ErrorMessage = "The name field is required")]
        public string name { get; set; }
        [DisplayName("帳號")]
        [Required(ErrorMessage = "The account field is required")]
        public string account { get; set; }
        [DisplayName("密碼")]
        [Required(ErrorMessage = "The password field is required")]
        public string password { get; set; }
        [DisplayName("權限")]
        public int role { get; set; }
        [DisplayName("創建時間")]
        public DateTime create_date { get; set; }
        [DisplayName("修改時間")]
        public DateTime update_date { get; set; }
        [DisplayName("修改人ID")]
        public int update_id { get; set; }                                        	
		                                
	}
}
