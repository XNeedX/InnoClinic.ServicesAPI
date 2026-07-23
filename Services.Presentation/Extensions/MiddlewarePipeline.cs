namespace Services.Presentation.Extensions;

public static class MiddlewarePipeline
{
    public static void ConfigureMiddlewarePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.OAuthClientId("public-client");
                options.OAuthAdditionalQueryStringParams(new Dictionary<string, string>
                    {
                        { "prompt", "login" }
                    });
            });
        }
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
    }
}
