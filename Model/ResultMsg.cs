namespace CodeTech.Model
{
    public class ResultMsg
    {
        public string s_msg = "Success";
        public string save_msg = "Saved Successfully";
        public string update_msg = "Updated Successfully";
        public string delete_msg = "Deleted Successfully";
        public bool s_status = true;
        public string err_msg = "Failed";
        public bool err_status = false;
        public string err_ct = "Error";
        public string id_err = "Provide Id for Update the value";
        public string err_token = "Invalid Token";
        public string dupl = "Item Duplication";
        public string err_code = "Code already exist";

        public string err_name = "Name already exist";
    }
}
