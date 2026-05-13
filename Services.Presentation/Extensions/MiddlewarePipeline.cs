namespace Services.Presentation.Extensions;

public static class MiddlewarePipeline
{
    public static void ConfigureMiddlewarePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
    }
}
