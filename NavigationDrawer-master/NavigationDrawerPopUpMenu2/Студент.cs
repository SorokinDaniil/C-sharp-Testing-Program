//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestingProgram
{
    using System;
    using System.Collections.Generic;
    
    public partial class Студент
    {
        public byte Id { get; set; }
        public string Имя { get; set; }
        public string Фамилия { get; set; }
        public string Пароль { get; set; }
        public byte Группа_Id { get; set; }
        public byte Результат_Id { get; set; }
        public string Логин { get; set; }
    
        public virtual Группа Группа { get; set; }
        public virtual Результат Результат { get; set; }
    }
}
