﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Datos;
using Capa_Entidad;

namespace Capa_Negocio
{
    public static class CN_Empleado
    {
        public static List<Empleado> Listar => CD_Empleado.Listar;
    }
}
