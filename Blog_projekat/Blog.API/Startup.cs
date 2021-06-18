using Blog.API.Controllers;
using Blog.API.Core;
using Blog.Application;
using Blog.Application.Commands.CategoryCommands;
using Blog.Application.Commands.UserCommands;
using Blog.DataAccess;
using Blog.Implementation.Commands.CategoryCommands;
using Blog.Implementation.Commands.UserCommands;
using Blog.Implementation.Logging;
using Blog.Implementation.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Implementation.Queries;
using Blog.Application.Queries;
using Blog.Application.Email;
using Blog.Implementation.Email;
using Blog.Implementation.Commands.CommentCommands;
using Blog.Application.Commands.CommentCommands;
using Blog.Implementation.Commands.PostCommands;
using Blog.Application.Commands.PostCommands;
using Blog.Implementation.Commands.RatingCommands;
using Blog.Application.Commands.RatingCommands;
using Blog.Implementation;

namespace Blog.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appSettings = new AppSettings();
            Configuration.Bind(appSettings);

        
            services.AddControllers();


            services.AddTransient<BlogContext>();
            services.AddTransient<CreateCategoryValidator>();
            services.AddTransient<UseCaseExecutor>();

            // ---------------------------------------------------------------------------------------------
            services.AddTransient<IUseCaseLogger, UseCaseLogger>();

            services.AddTransient<ICreateCategoryCommand, CreateCategoryCommand>();
            services.AddTransient<IDeleteCategoryCommand, DeleteCategoryCommand>();
            services.AddTransient<IUpdateCategoryCommand, UpdateCategoryCommand>();


            services.AddTransient<ICreateCommentCommand, CreateCommentCommand>();
            services.AddTransient<IDeleteCommentCommand, DeleteCommentCommand>();
            services.AddTransient<IUpdateCommentCommand, UpdateCommentCommand>();

            services.AddTransient<ICreatePostCommand, CreatePostCommand>();
            services.AddTransient<IDeletePostCommand, DeletePostCommand>();
            services.AddTransient<IUpdatePostCommand, UpdatePostCommand>();


            services.AddTransient<IAddRatingToPostCommand, AddRatingToPostCommand>();


            services.AddTransient<IRegisterUserCommand, UserRegisterCommand>();
            services.AddTransient<IUpdateUserCommand, UpdateUserCommand>();
            services.AddTransient<IDeleteUserCommand, DeleteUserCommand>();



            // ---------------------------------------------------------------------------------------------



            /* VALIDATORS */
            services.AddTransient<RegisterUserValidator>();
            services.AddTransient<UpdateUserValidator>();

            services.AddTransient<CreateCategoryValidator>();
            services.AddTransient<UpdateCategoryValidator>();

            services.AddTransient<CreatePostValidator>();
            services.AddTransient<UpdatePostValidator>();

            services.AddTransient<CreateCommentValidator>();
            services.AddTransient<UpdateCommentValidator>();

            services.AddTransient<AddRatingToPostValidator>();




            services.AddTransient<IEmailSender, SmtpEmailSender>(x => new SmtpEmailSender(appSettings.EmailFrom, appSettings.EmailPassword));



            services.AddHttpContextAccessor();
            //------------------------------------------------------------------------------------------


            services.AddJwt(appSettings);





            services.AddTransient<IGetCategoriesQuery, GetCategoriesQuery>();
            services.AddTransient<IGetCommentsQuery, GetCommentsQuery>();
            services.AddTransient<IGetLogsQuery, GetLogsQuery>();
            services.AddTransient<IGetPostsQuery, GetPostsQuery>();
            services.AddTransient<IGetSentMailsQuery, GetSentMailsQuery>();
            services.AddTransient<IGetUsersQuery, GetUsersQuery>();

            services.AddTransient<IGetCategoryQuery, GetCategoryQuery>();
            services.AddTransient<IGetCommentQuery, GetCommentQuery>();
            services.AddTransient<IGetLogQuery, GetLogQuery>();
            services.AddTransient<IGetPostQuery, GetPostQuery>();
            services.AddTransient<IGetSentMailQuery, GetSentMailQuery>();
            services.AddTransient<IGetUserQuery, GetUserQuery>();







            //------------------------------- AUTOMAPPER -----------------------------------------

            //services.AddAutoMapper(typeof(GetCategoriesQuery).Assembly);
            //services.AddAutoMapper(typeof(GetCommentsQuery).Assembly);
            //services.AddAutoMapper(typeof(GetLogsQuery).Assembly);
            //services.AddAutoMapper(typeof(GetPostsQuery).Assembly);
            //services.AddAutoMapper(typeof(GetSentMailsQuery).Assembly);
            //services.AddAutoMapper(typeof(GetUsersQuery).Assembly);

            //------------------------------------------------------------------------------------------


            services.AddTransient<IApplicationActor>(x =>
            {
                var accessor = x.GetService<IHttpContextAccessor>();
              

                var user = accessor.HttpContext.User;

                if (user.FindFirst("ActorData") == null)
                {
                    return new UnauthorizedActor();
                }

                var actorString = user.FindFirst("ActorData").Value;

                var actor = JsonConvert.DeserializeObject<JwtActor>(actorString);

                return actor;

            });

            /*---------------------------------------------- SWAGGER -------------------------------------------------*/

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Blog Projekat", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                          {
                            Reference = new OpenApiReference
                              {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                              },
                              Scheme = "oauth2",
                              Name = "Bearer",
                              In = ParameterLocation.Header,

                            },
                            new List<string>()
                          }
                    });
            });

            /*-------------------------------------------------------------------------------------------------------*/



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
               
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger"));
            app.UseCors(x =>
            {
                x.AllowAnyOrigin();
                x.AllowAnyMethod();
                x.AllowAnyHeader();
            });
            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseStaticFiles();
            app.UseMiddleware<ExceptionHandler>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        
        }
    }
}

