using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KinXub.Models 
{
	public class Member                            
	{
        [DisplayName("ID")]
        public int id { get; set; }
        [DisplayName("���Q")]
        [Required(ErrorMessage = "The name field is required")]
        public string name { get; set; }
        [DisplayName("��̖")]
        [Required(ErrorMessage = "The account field is required")]
        public string account { get; set; }
        [DisplayName("�ܴa")]
        [Required(ErrorMessage = "The password field is required")]
        public string password { get; set; }
        [DisplayName("����")]
        public int role { get; set; }
        [DisplayName("�����r�g")]
        public DateTime create_date { get; set; }
        [DisplayName("�޸ĕr�g")]
        public DateTime update_date { get; set; }
        [DisplayName("�޸���ID")]
        public int update_id { get; set; }                                        	
		                                
	}
}
