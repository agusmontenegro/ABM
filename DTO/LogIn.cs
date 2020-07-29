namespace ABM.DTO
{
    class LogIn
    {
        public bool LoginSuccess { get; set; }
        public bool MultiProfile { get; set; }
        public int IdUsuario { get; set; }
        public int IdRol { get; set; }
        public User Usuario { get; set; }
        public string ErrorMessage { get; set; }
    }
}
