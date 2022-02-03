﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Snt.Romashka.Host
{
    public static class KestrelServerOptionsExtensions
    {
        public static void ConfigureEndpoints(this KestrelServerOptions options)
        {
            var configuration = options.ApplicationServices.GetRequiredService<IConfiguration>();
            var environment = options.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

            var endpoints = configuration.GetSection("HttpServer:Endpoints")
                .GetChildren()
                .ToDictionary(section => section.Key, section =>
                {
                    var endpoint = new EndpointConfiguration();
                    section.Bind(endpoint);
                    return endpoint;
                });

            foreach (var endpoint in endpoints)
            {
                var config = endpoint.Value;
                var port = config.Port ?? (config.Scheme == "https" ? 443 : 80);

                var ipAddresses = new List<IPAddress>();
                if (config.Host == "localhost")
                {
                    ipAddresses.Add(IPAddress.IPv6Loopback);
                    ipAddresses.Add(IPAddress.Loopback);
                }
                else if (IPAddress.TryParse(config.Host, out var address))
                {
                    ipAddresses.Add(address);
                }
                else
                {
                    ipAddresses.Add(IPAddress.IPv6Any);
                }

                foreach (var address in ipAddresses)
                {
                    options.Listen(address, port,
                        listenOptions =>
                        {
                            if (config.Scheme == "https")
                            {
                                var certificate = LoadCertificate(config, environment);
                                listenOptions.UseHttps(certificate);
                            }
                        });
                }
            }
        }

        private static X509Certificate2 LoadCertificate(EndpointConfiguration config, IWebHostEnvironment environment)
        {
            if (config.StoreName != null && config.StoreLocation != null)
            {
                using (var store = new X509Store(config.StoreName, Enum.Parse<StoreLocation>(config.StoreLocation)))
                {
                    store.Open(OpenFlags.ReadOnly);
                    var certificate = store.Certificates.Find(
                        X509FindType.FindBySubjectName,
                        config.Host,
                        validOnly: !environment.IsDevelopment());

                    if (certificate.Count == 0)
                    {
                        throw new InvalidOperationException($"Certificate not found for {config.Host}.");
                    }

                    return certificate[0];
                }
            }

            if (config.FilePath != null && config.Password != null)
            {
                return new X509Certificate2(Path.GetFullPath(config.FilePath), config.Password);
            }

            throw new InvalidOperationException("No valid certificate configuration found for the current endpoint.");
        }
    }

    public class EndpointConfiguration
    {
        /// <summary>
        /// Имя сервера или IP адрес
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Порт
        /// </summary>
        public int? Port { get; set; }

        /// <summary>
        /// Схема, http или https
        /// </summary>
        public string Scheme { get; set; }

        /// <summary>
        /// Наименование хранилища сертификата, по умолчанию My (личное)
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// Тип хранилища, по умолчанию CurrentUser (текущий пользователь)
        /// </summary>
        public string StoreLocation { get; set; }

        /// <summary>
        /// Путь к файлу сертификата. Используется, если не указано хранилище и тип хранилища
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Пароль к файлу сертификата. Используется, если не указано хранилище и тип хранилища
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// URL домена, с которого будут приходить запросы к сервису, для их разрешения
        /// </summary>
        public string CorsOrigins { get; set; }
    }
}