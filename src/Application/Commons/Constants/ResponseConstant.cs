namespace Application.Commons.Constants;

public class ResponseConstant
{
    public const string Success = "La operación se completo correctamente.";
    
    public const string NotFound = "No se encontró la información.";
    
    public const string Error = "Ocurrió un error en el proceso.";
        
    public const string Opt = "Le hemos enviado el código de activación para completar su registro.";

    public const string SignUpSuccess = "En breve recibira un correo electronico para confirmar su cuenta";
        
    public const string Confirm = "Hemos enviado una notificación para la activación de tu cuenta, revisa tu correo electrónico.";

    public const string ActivationSuccess = "Su cuenta ha sido activada correctamente, puede iniciar sesión.";

    public const string ChangePassword = "Su contraseña se actualizó exitosamente, puede iniciar sesión con su nueva contraseña.";

    public const string SuccessPassword = "Su contraseña se actualizó exitosamente, inicie sesión con su nueva contraseña.";

    public const string LockedAccount = "Parece que has superado el número máximo de intentos fallidos. Vuelve a intentarlo en unos minutos.";
        
    public const string SignInFail = "Correo electrónico o contraseña incorrectos, verifíquelos e inténtelo nuevamente.";

    public const string TwoFactorFail = "El código es incorrecto, verifíquelo o genere un nuevo código de autenticación.";

    public const string TwoFactorError = "Token incorrecto, inténtalo de nuevo.";
        
    public const string Empty = "No se encontró información, con los parámetros enviados.";
        
    public const string Fail = "Se produjo un error en el proceso.";

    public const string ErrorProvider = "Error del proveedor de inicio de sesión externo.";

    public const string UserNotFound = "El Usuario enviado no existe.";
    
    public const string EmployeeNotFound = "El empleado enviado no existe.";
    
    public const string FailRefreshToken = "Refresh token invalido.";

    public const string RecoveryPassword = "Hemos enviado las instrucciones por correo electrónico para seguir y recuperar la contraseña.";

    public const string DocumentIsAlreadyRegistered = "Documento ya se encuentra registrado.";
}