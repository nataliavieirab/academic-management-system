namespace EscolaDeCursos.Dominio.Compartilhado.Identity;

public interface IUserProvider
{
    public Guid? Id { get; }
    bool EstaAutenticado { get; }
}
