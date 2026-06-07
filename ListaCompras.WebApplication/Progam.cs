using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ListaCompras.WebApplication.Compartilhado; // ESTE USING É OBRIGATÓRIO

var builder = WebApplication.CreateBuilder(args);

// Aqui o compilador agora encontrará o método "AddInfrastructureAndApplication" (ou AddPresentation)
builder.Services.AddInfrastructureAndApplication();

var app = builder.Build();

// 3. Pipeline de requisição HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// 4. Mapeamento de rotas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ListaCompras}/{action=Listar}/{id?}");

app.Run();