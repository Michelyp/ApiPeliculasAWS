using ApiPeliculasAWS.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiPeliculasAWS.Models
{
    public class RepositoryPeliculas
    {

        private PeliculasContext context;
        public RepositoryPeliculas(PeliculasContext context)
        {
            this.context = context;
        }
        public async Task<List<Pelicula>> GetPeliculasAsync()
        {
            return await this.context.Peliculas.ToListAsync();
        }
        public async Task<List<Pelicula>> GetPeliculasActorAsync(string actor)
        {
            return await this.context.Peliculas.Where(x => x.Actores.Contains(actor)).ToListAsync();

        }
        public async Task<Pelicula> FindPeliculaAsync(int idPelicula) 
        {
            return await this.context.Peliculas.FirstOrDefaultAsync(x => x.IdPelicula == idPelicula);
        }
        public async Task<int> GetMaxIdPeliculasAsync()
        {
            return await this.context.Peliculas.MaxAsync(x => x.IdPelicula)+1;
        }
        public async Task CreatePeliculaAsync(string genero, string titulo, string argumento, string foto,string actores
            ,int precio, string youtube)
        {
            Pelicula pelicula = new Pelicula
            {
                IdPelicula = await this.GetMaxIdPeliculasAsync(),
                Genero = genero,
                Titulo = titulo,
                Argumento = argumento,
                Foto = foto,
                Actores = actores,
                Precio = precio,
                Youtube = youtube
            };
            this.context.Peliculas.Add(pelicula);
            await this.context.SaveChangesAsync();
        }
        public async Task UpdatePeliculaAsync(int idpelicula, string genero, string titulo, string argumento, string foto, string actores
            , int precio, string youtube)
        {

            Pelicula pelicula = await this.FindPeliculaAsync(idpelicula);
            pelicula.Genero = genero;
            pelicula.Titulo = titulo;
            pelicula.Argumento = argumento;
            pelicula.Foto = foto;
            pelicula.Actores = actores;
            pelicula.Precio = precio;
            pelicula.Youtube = youtube;
            await this.context.SaveChangesAsync();

        }
        public async Task DeletePeliculaAsync(int idpelicula)
        {
            Pelicula pelicula = await this.FindPeliculaAsync(idpelicula);
            this.context.Peliculas.Remove(pelicula);
            await this.context.SaveChangesAsync();
        }

    }
}
