﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlogLab.Models;

namespace BlogLab.Controllers
{
    public class BlogPostsController : Controller
    {
        private BlogDBContext db = new BlogDBContext();

        // GET: BlogPosts
        public ActionResult Index()
        {
            var model = db.BlogPosts.OrderByDescending(d => d.CreateDate).Take(5).ToList();
            return View(model);
        }

        // GET: BlogPosts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.BlogPosts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            try
            {
                ViewBag.PrevPost =
                    db.BlogPosts.Where(x => x.CreateDate < blogPost.CreateDate)
                        .OrderByDescending(t => t.CreateDate)
                        .First()
                        .Id;
            }
            catch
            {
            }
            try
            {
                ViewBag.NextPost =
                    db.BlogPosts.Where(x => x.CreateDate > blogPost.CreateDate).OrderBy(t => t.CreateDate).First().Id;
            }
            catch (Exception)
            {
            }

            return View(blogPost);
        }

        // GET: BlogPosts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogPosts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Body,Author,ImageLink")] BlogPost blogPost)
        {
            blogPost.CreateDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.BlogPosts.Add(blogPost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blogPost);
        }

        // GET: BlogPosts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.BlogPosts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // POST: BlogPosts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Body,Author,ImageLink")] BlogPost blogPost)
        {
            if (!ModelState.IsValid)
            {
                return View(blogPost);
            }

            BlogPost dbBlogPost = db.BlogPosts.Find(blogPost.Id);
            if (dbBlogPost == null)
            {
                return RedirectToAction("Index");
            }

            dbBlogPost.ImageLink = blogPost.ImageLink;
            dbBlogPost.Author = blogPost.Author;
            dbBlogPost.Body = blogPost.Body;
            dbBlogPost.Title = blogPost.Title;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: BlogPosts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.BlogPosts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // POST: BlogPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogPost blogPost = db.BlogPosts.Find(id);
            db.BlogPosts.Remove(blogPost);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
