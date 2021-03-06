﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Bicikli_Admin.CommonClasses;
using Bicikli_Admin.Models;

namespace Bicikli_Admin.Controllers
{
    [Authorize(Roles = "SiteAdmin")]
    public class UsersController : Controller
    {
        //
        // GET: /Users/

        public ActionResult Index()
        {
            ViewBag.active_menu_item_id = "menu-btn-users";
            return View(DataRepository.GetUsersWithDetails());
        }

        //
        // GET: /Users/SiteAdminsList

        public ActionResult SiteAdminsList()
        {
            ViewBag.active_menu_item_id = "menu-btn-users";
            return View(DataRepository.GetUsersWithDetails().Where(u => u.isSiteAdmin));
        }

        //
        // GET: /Users/UnassignedList

        public ActionResult UnassignedList()
        {
            ViewBag.active_menu_item_id = "menu-btn-users";
            return View(DataRepository.GetUsersWithDetails().Where(u => u.countOfLenders == 0));
        }

        //
        // GET: /Users/UnapprovedList

        public ActionResult UnapprovedList()
        {
            ViewBag.active_menu_item_id = "menu-btn-users";
            return View(DataRepository.GetUsersWithDetails().Where(u => !u.isApproved));
        }

        //
        // GET: /Users/BannedList

        public ActionResult BannedList()
        {
            ViewBag.active_menu_item_id = "menu-btn-users";
            return View(DataRepository.GetUsersWithDetails().Where(u => u.isLockedOut));
        }

        //
        // GET: /Users/Details/5

        public ActionResult Details(string id)
        {
            UserModel model;
            try
            {
                model = DataRepository.GetUsersWithDetails().Single(u => u.username == id);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            try
            {
                ViewBag.active_menu_item_id = "menu-btn-users";
                ViewBag.SelectedLenders = DataRepository.GetLendersOfUser(model.guid);
                return View(model);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Users/Edit/5

        public ActionResult Edit(string id)
        {
            UserModel model;
            try
            {
                model = DataRepository.GetUsersWithDetails().Single(u => u.username == id);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            try
            {
                ViewBag.active_menu_item_id = "menu-btn-users";
                ViewBag.SelectedLenders = DataRepository.GetLendersOfUser(model.guid);
                ViewBag.Lenders = DataRepository.GetLenders();
                return View(model);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /Users/Edit/5

        [HttpPost]
        public ActionResult Edit(UserModel model, int[] lenders)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception();
                }

                try
                {
                    DataRepository.UpdateUser(model);
                }
                catch
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }
                DataRepository.UpdateUserAssignments(model.username, lenders);
                
                return RedirectToAction("Index");
            }
            catch
            {
                #region Redisplay page

                ViewBag.active_menu_item_id = "menu-btn-users";
                var user = DataRepository.GetUsersWithDetails().Single(u => u.username == model.username);
                model.lastLogin = user.lastLogin;
                var dbLenders = DataRepository.GetLenders();
                ViewBag.Lenders = dbLenders;

                var selectedLenders = new List<LenderModel>();
                foreach (int lid in lenders)
                {
                    selectedLenders.Add(
                        new LenderModel
                        {
                            name = dbLenders.Where(dbl => dbl.id == lid).First().name,
                            id = lid
                        });
                }
                ViewBag.SelectedLenders = selectedLenders;

                #endregion

                return View(model);
            }
        }

    }
}
