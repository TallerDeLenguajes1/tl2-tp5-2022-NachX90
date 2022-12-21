namespace CadeteriaMVC.Enums
{
    public enum Errores
    {
        LogueoIncorrecto,
        CerrarSesionExito,
        CerrarSesionCerrada,
        AccesoDenegado,
        ModeloInvalido,
    }

    public static class Mensajes
    {
        public static string MostrarError(Errores EnumErrores)
        {
            switch (EnumErrores)
            {
                case Errores.LogueoIncorrecto:
                    return "Los datos ingresados son incorrectos. Por favor intente nuevamente";
                case Errores.CerrarSesionExito:
                    return "La sesion se cerro correctamente";
                case Errores.CerrarSesionCerrada:
                    return "La sesion ya estaba cerrada";
                case Errores.AccesoDenegado:
                    return "No tiene permisos suficientes";
                case Errores.ModeloInvalido:
                    return "Hubo un problema al procesar los datos. Por favor intente nuevamente";
                default:
                    return "Ups! Conctacta con el administrador";
            }
        }
    }
}
