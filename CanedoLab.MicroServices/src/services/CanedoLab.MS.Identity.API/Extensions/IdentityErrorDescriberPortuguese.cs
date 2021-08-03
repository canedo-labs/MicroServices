using Microsoft.AspNetCore.Identity;

namespace CanedoLab.MS.Identity.API.Extensions
{
    public class IdentityErrorDescriberPortuguese : IdentityErrorDescriber
    {
        public override IdentityError DefaultError()
            => CreateIdentityError(nameof(DefaultError), $"Ocorreu um erro desconhecido.");
        public override IdentityError ConcurrencyFailure()
            => CreateIdentityError(nameof(ConcurrencyFailure), "Falha de concorrência otimista, o objeto foi modificado.");
        public override IdentityError PasswordMismatch()
            => CreateIdentityError(nameof(PasswordMismatch), "Senha incorreta.");
        public override IdentityError InvalidToken()
            => CreateIdentityError(nameof(InvalidToken), "Token inválido.");
        public override IdentityError LoginAlreadyAssociated()
            => CreateIdentityError(nameof(LoginAlreadyAssociated), "Já existe um usuário com este login.");
        public override IdentityError InvalidUserName(string userName)
            => CreateIdentityError(nameof(InvalidUserName), $"O login '{userName}' é inválido, pode conter apenas letras ou dígitos.");
        public override IdentityError InvalidEmail(string email)
            => CreateIdentityError(nameof(InvalidEmail), $"O email '{email}' é inválido.");
        public override IdentityError DuplicateUserName(string userName)
            => CreateIdentityError(nameof(DuplicateUserName), $"O login '{userName}' já está sendo utilizado.");
        public override IdentityError DuplicateEmail(string email)
            => CreateIdentityError(nameof(DuplicateEmail), $"O email '{email}' já está sendo utilizado.");
        public override IdentityError InvalidRoleName(string role)
            => CreateIdentityError(nameof(InvalidRoleName), $"A permissão '{role}' é inválida.");
        public override IdentityError DuplicateRoleName(string role)
            => CreateIdentityError(nameof(DuplicateRoleName), $"A permissão '{role}' já está sendo utilizada.");
        public override IdentityError UserAlreadyHasPassword()
            => CreateIdentityError(nameof(UserAlreadyHasPassword), "O usuário já possui uma senha definida.");
        public override IdentityError UserLockoutNotEnabled()
            => CreateIdentityError(nameof(UserLockoutNotEnabled), "O lockout não está habilitado para este usuário.");
        public override IdentityError UserAlreadyInRole(string role)
            => CreateIdentityError(nameof(UserAlreadyInRole), $"O usuário já possui a permissão '{role}'.");
        public override IdentityError UserNotInRole(string role)
            => CreateIdentityError(nameof(UserNotInRole), $"O usuário não tem a permissão '{role}'.");
        public override IdentityError PasswordTooShort(int length)
            => CreateIdentityError(nameof(PasswordTooShort), $"As senhas devem conter ao menos {length} caracteres.");
        public override IdentityError PasswordRequiresNonAlphanumeric()
            => CreateIdentityError(nameof(PasswordRequiresNonAlphanumeric), "As senhas devem conter ao menos um caracter não alfanumérico.");
        public override IdentityError PasswordRequiresDigit() 
            => CreateIdentityError(nameof(PasswordRequiresDigit), "As senhas devem conter ao menos um digito ('0'-'9').");
        public override IdentityError PasswordRequiresLower()
            => CreateIdentityError(nameof(PasswordRequiresLower), "As senhas devem conter ao menos um caracter em caixa baixa ('a'-'z').");
        public override IdentityError PasswordRequiresUpper()
            => CreateIdentityError(nameof(PasswordRequiresUpper), "As senhas devem conter ao menos um caracter em caixa alta ('A'-'Z').");

        private static IdentityError CreateIdentityError(string code, string description) => new() { Code = code, Description = description };
    }
}
