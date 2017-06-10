using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DAL.Repositories;
using DAL.Rest.Helpers;
using Microsoft.AspNetCore.Http;

namespace DAL.Rest.Repositories
{
    public class RestRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        protected HttpClient HttpClient;
        protected string EndPoint;
        private DataContractJsonSerializer _serializer;

        public RestRepository(HttpClient httpClient, string endPoint)
        {
            HttpClient = httpClient ?? throw new ArgumentNullException(paramName: nameof(HttpClient), message: "Web API baserepo repo for type:" + typeof(TEntity).FullName);
            EndPoint = endPoint ?? throw new ArgumentNullException(paramName: nameof(endPoint), message: "Web API baserepo repo for type:" + typeof(TEntity).FullName);
        }


        public virtual IEnumerable<TEntity> All()
        {
            var response = HttpClient.GetAsync(requestUri: EndPoint).Result;

            if (response.IsSuccessStatusCode)
            {
                _serializer = new DataContractJsonSerializer(type: typeof(List<TEntity>));
                var res =_serializer.ReadObject(stream: response.Content.ReadAsStreamAsync().Result) as List<TEntity>;
                return res;
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                SignOutAndRedirectToLogin();
            }

            // return empty list. or null?
            return new List<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> AllAsync()
        {
            var response = await HttpClient.GetAsync(requestUri: EndPoint);

            if (response.IsSuccessStatusCode)
            {
                _serializer = new DataContractJsonSerializer(type: typeof(List<TEntity>));
                var res = _serializer.ReadObject(stream: await response.Content.ReadAsStreamAsync()) as List<TEntity>;
                return res;
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                SignOutAndRedirectToLogin();
            }

            // return empty list. or null?
            return new List<TEntity>();
        }


        public virtual TEntity Find(params object[] id)
        {
            // baseurl/repo/action/id0/id1/id2/...
            var uri = string.Join(separator: "/",values: id);

            var response = HttpClient.GetAsync(requestUri: EndPoint + uri).Result;

            if (response.IsSuccessStatusCode)
            {
                _serializer = new DataContractJsonSerializer(type: typeof(TEntity));
                var res = _serializer.ReadObject(stream: response.Content.ReadAsStreamAsync().Result) as TEntity;
                return res;
            }
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                SignOutAndRedirectToLogin();
            }

            return null;
        }

        public virtual async Task<TEntity> FindAsync(params object[] id)
        {
            // baseurl/repo/action/id0/id1/id2/...
            var uri = string.Join(separator: "/", values: id);

            var response = await HttpClient.GetAsync(requestUri: EndPoint + uri);

            if (response.IsSuccessStatusCode)
            {
                _serializer = new DataContractJsonSerializer(type: typeof(TEntity));
                var res = _serializer.ReadObject(stream: await response.Content.ReadAsStreamAsync()) as TEntity;
                return res;
            }
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                SignOutAndRedirectToLogin();
            }

            return null;
        }

        public virtual async Task<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken)
        {
            // baseurl/repo/action/id0/id1/id2/...
            var uri = string.Join(separator: "/", values: keyValues);

            var response = await HttpClient.GetAsync(requestUri: EndPoint + uri, cancellationToken: cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                _serializer = new DataContractJsonSerializer(type: typeof(TEntity));
                var res = _serializer.ReadObject(stream: await response.Content.ReadAsStreamAsync()) as TEntity;
                return res;
            }
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                SignOutAndRedirectToLogin();
            }

            return null;
        }

        public virtual void Add(TEntity entity)
        {
            _serializer = new DataContractJsonSerializer(type: typeof(TEntity));

            var memoryStream = new MemoryStream();
            _serializer.WriteObject(stream: memoryStream, graph: entity);

            var content = new StreamContent(content: memoryStream);
            content.Headers.ContentEncoding.Add(item: "UTF8");
            content.Headers.ContentType = new MediaTypeHeaderValue(mediaType: "application/json");

            var response = HttpClient.PostAsync(requestUri: EndPoint,content: content).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(message: response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
            }
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                SignOutAndRedirectToLogin();
            }
            // TODO: update entity key?
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            _serializer = new DataContractJsonSerializer(type: typeof(TEntity));

            var memoryStream = new MemoryStream();
            _serializer.WriteObject(stream: memoryStream, graph: entity);

            var content = new StreamContent(content: memoryStream);
            content.Headers.ContentEncoding.Add(item: "UTF8");
            content.Headers.ContentType = new MediaTypeHeaderValue(mediaType: "application/json");

            var response = await HttpClient.PostAsync(requestUri: EndPoint, content: content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(message: response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
            }
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                SignOutAndRedirectToLogin();
            }
            // TODO: update entity key?
        }

        public virtual TEntity Update(TEntity entity)
        {
            var keys = GetEntityKeys(entity: entity).OrderBy(keySelector: k => k.Order).ToArray();
            if (keys == null || keys.Length == 0)
            {
                throw new KeyNotFoundException(message: "Primary key(s) not detected in entity type: " + typeof(TEntity).FullName);
            }
            var uri = string.Join(separator: "/", values: keys.Select(selector: a => a.Value.ToString()).ToList());

            uri = EndPoint + uri;

            _serializer = new DataContractJsonSerializer(type: typeof(TEntity));

            var memoryStream = new MemoryStream();
            _serializer.WriteObject(stream: memoryStream, graph: entity);

            var content = new StreamContent(content: memoryStream);
            content.Headers.ContentEncoding.Add(item: "UTF8");
            content.Headers.ContentType = new MediaTypeHeaderValue(mediaType: "application/json");


            var response = HttpClient.PutAsync(requestUri: uri, content: content).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(message: response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                SignOutAndRedirectToLogin();
            }

            return entity;
        }

        public virtual void Remove(TEntity entity)
        {
            var keys = GetEntityKeys(entity: entity).OrderBy(keySelector: k => k.Order).ToArray();
            if (keys == null || keys.Length == 0)
            {
                throw new KeyNotFoundException(message: "Primary key(s) not detected in entity type: " + typeof(TEntity).FullName);
            }
            var uri = string.Join(separator: "/", values: keys.Select(selector: a => a.Value.ToString()).ToList());

            uri = EndPoint + uri;

            var response = HttpClient.DeleteAsync(requestUri: uri).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(message: response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                SignOutAndRedirectToLogin();
            }
        }

        public virtual void Remove(params object[] id)
        {
            var uri = string.Join(separator: "/", values: id);

            uri = EndPoint + uri;

            var response = HttpClient.DeleteAsync(requestUri: uri).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(message: response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                SignOutAndRedirectToLogin();
            }
        }

        public virtual void SignOutAndRedirectToLogin()
        {
            // TODO Sign out of MVC, redireect client to login page. Probably use delegate?   

            //_authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            ////redirect client to login page somehow
            //HttpContext.Current.Response.ClearContent();
            //HttpContext.Current.Response.Redirect(@"~/Account/Login");
        }


        public List<EntityKeyInfo> GetEntityKeys(TEntity entity)
        {
            var res = new List<EntityKeyInfo>();

            var className = typeof(TEntity).Name.ToLower();
            var properties = typeof(TEntity).GetProperties();

            foreach (var propertyInfo in properties)
            {
                var columOrder = 0;
                var isKey = false;

                // lets check the [Key] and [Column(Order=xx)] attributes
                var attrs = propertyInfo.GetCustomAttributes(inherit: true);
                foreach (Attribute attr in attrs)
                {
                    if (attr is KeyAttribute)
                    {
                        isKey = true;
                    }

                    var attribute = attr as ColumnAttribute;
                    if (attribute != null)
                    {
                        columOrder = attribute.Order;
                    }
                }

                if (isKey)
                {
                    res.Add(item: new EntityKeyInfo(propertyName: propertyInfo.Name, value: propertyInfo.GetValue(obj: entity, index: null), order: columOrder));
                }
            }

            // if key(s) are already found, return
            if (res.Count > 0) return res;

            // no keys yet, check for property name
            foreach (var propertyInfo in properties)
            {
                var name = propertyInfo.Name.ToLower();
                if (name.Equals(value: className + "id") || name.Equals(value: "id"))
                {
                    res.Add(item: new EntityKeyInfo()
                    {
                        PropertyName = propertyInfo.Name,
                        Value = propertyInfo.GetValue(obj: entity, index: null)
                    });
                }

            }

            return res;
        }
    }
}
