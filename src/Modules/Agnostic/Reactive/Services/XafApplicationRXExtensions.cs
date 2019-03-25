﻿using System;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using Xpand.XAF.Modules.Reactive.Extensions;

namespace Xpand.XAF.Modules.Reactive.Services{
    public static class XafApplicationRXExtensions{

        public static void RegisterAsRX(this XafApplication application) {
            RxApp.XafApplication=application;
        }

        public static void AddObjectSpaceProvider(this XafApplication application, IObjectSpaceProvider objectSpaceprovider) {
            application.WhenCreateCustomObjectSpaceProvider()
                .Select(_ => {
                    _.e.ObjectSpaceProviders.Add(objectSpaceprovider);
                    return _;
                })
                .Subscribe();
        }

        public static IObservable<ITypesInfo> WhenCustomizingTypesInfo(this XafApplication application) {
            return application.Modules.OfType<ReactiveModule>().ToObservable(Scheduler.Default)
                .Repeat()
                .FirstAsync()
                .Select(_ => _.TypesInfo).Switch();
        }

        public static IObservable<(XafApplication application, CreateCustomObjectSpaceProviderEventArgs e)> WhenCreateCustomObjectSpaceProvider(this XafApplication application){
            return Observable
                .FromEventPattern<EventHandler<CreateCustomObjectSpaceProviderEventArgs>,CreateCustomObjectSpaceProviderEventArgs>(h => application.CreateCustomObjectSpaceProvider += h,h => application.CreateCustomObjectSpaceProvider -= h)
                .TakeUntilDisposingMainWindow()
                .TransformPattern<CreateCustomObjectSpaceProviderEventArgs,XafApplication>();
        }

        public static IObservable<(XafApplication application, ObjectSpaceCreatedEventArgs e)> ObjectSpaceCreated(this IObservable<XafApplication> source){
            return source.SelectMany(application => application.WhenObjectSpaceCreated());
        }

        public static IObservable<(XafApplication application, ObjectSpaceCreatedEventArgs e)> WhenObjectSpaceCreated(this XafApplication application){
            return Observable
                .FromEventPattern<EventHandler<ObjectSpaceCreatedEventArgs>,ObjectSpaceCreatedEventArgs>(h => application.ObjectSpaceCreated += h,h => application.ObjectSpaceCreated -= h)
                .TakeUntilDisposingMainWindow()
                .TransformPattern<ObjectSpaceCreatedEventArgs,XafApplication>();
        }

        public static IObservable<(XafApplication application, ViewCreatedEventArgs e)> ViewCreated(
            this IObservable<XafApplication> source){
            return source.SelectMany(application => application.WhenViewCreated());
        }

        public static IObservable<(XafApplication application, DetailViewCreatedEventArgs e)> WhenDetailViewCreated(this XafApplication application){
            return Observable
                .FromEventPattern<EventHandler<DetailViewCreatedEventArgs>, DetailViewCreatedEventArgs>(
                    h => application.DetailViewCreated += h, h => application.DetailViewCreated -= h)
                .TakeUntilDisposingMainWindow()
                .TransformPattern<DetailViewCreatedEventArgs, XafApplication>();
        }

        public static IObservable<(XafApplication application, ListViewCreatedEventArgs e)> ListViewCreated(this IObservable<XafApplication> source){
            return source.SelectMany(application => application.WhenListViewCreated());
        }
        public static IObservable<(XafApplication application, ListViewCreatedEventArgs e)> WhenListViewCreated(this XafApplication application){
            return Observable
                .FromEventPattern<EventHandler<ListViewCreatedEventArgs>, ListViewCreatedEventArgs>(
                    h => application.ListViewCreated += h, h => application.ListViewCreated -= h)
                .TakeUntilDisposingMainWindow()
                .TransformPattern<ListViewCreatedEventArgs, XafApplication>();
        }
        public static IObservable<(XafApplication application, ViewCreatedEventArgs e)> WhenObjectViewCreated(this XafApplication application){
            return application.AsObservable().ObjectViewCreated();
        }

        public static IObservable<(XafApplication application, DetailViewCreatedEventArgs e)> DetailViewCreated(this IObservable<XafApplication> source){
            return source.SelectMany(application => application.WhenDetailViewCreated());
        }
        public static IObservable<(XafApplication application, ViewCreatedEventArgs e)> ObjectViewCreated(this IObservable<XafApplication> source){
            return source.ViewCreated().Where(_ => _.e.View is ObjectView);
        }

        public static IObservable<(XafApplication application, ViewCreatedEventArgs e)> WhenViewCreated(this XafApplication application){
            return Observable
                .FromEventPattern<EventHandler<ViewCreatedEventArgs>,ViewCreatedEventArgs>(h => application.ViewCreated += h,h => application.ViewCreated -= h)
                .TakeUntilDisposingMainWindow()
                .TransformPattern<ViewCreatedEventArgs,XafApplication>();
        }

        public static IObservable<(XafApplication application, DatabaseVersionMismatchEventArgs e)> AlwaysUpdateOnDatabaseVersionMismatch(this XafApplication application){
            return application.WhenDatabaseVersionMismatch().Select(tuple => {
                tuple.e.Updater.Update();
                tuple.e.Handled = true;
                return tuple;
            })
            .TakeUntilDisposingMainWindow();
        }

        public static IObservable<(XafApplication application, DatabaseVersionMismatchEventArgs e)> WhenDatabaseVersionMismatch(this XafApplication application){
            return Observable
                .FromEventPattern<EventHandler<DatabaseVersionMismatchEventArgs>,DatabaseVersionMismatchEventArgs>(h => application.DatabaseVersionMismatch += h,h => application.DatabaseVersionMismatch -= h)
                .TakeUntilDisposingMainWindow()
                .TransformPattern<DatabaseVersionMismatchEventArgs,XafApplication>();
        }

        public static IObservable<(XafApplication application, LogonEventArgs e)> WhenLoggedOn(this XafApplication application){
            return Observable
                .FromEventPattern<EventHandler<LogonEventArgs>,LogonEventArgs>(h => application.LoggedOn += h,h => application.LoggedOn -= h)
                .TakeUntilDisposingMainWindow()
                .TransformPattern<LogonEventArgs,XafApplication>();
        }

        public static IObservable<(XafApplication application, EventArgs e)> WhenSetupComplete(this XafApplication application){
            return Observable
                .FromEventPattern<EventHandler<EventArgs>,
                    EventArgs>(h => application.SetupComplete += h,h => application.SetupComplete -= h).TakeUntilDisposingMainWindow()
                .TransformPattern<EventArgs,XafApplication>();
        }

        public static IObservable<(XafApplication application, CreateCustomModelDifferenceStoreEventArgs e)> WhenCreateCustomModelDifferenceStore(this XafApplication application){
            return Observable
                .FromEventPattern<EventHandler<CreateCustomModelDifferenceStoreEventArgs>,CreateCustomModelDifferenceStoreEventArgs>(h => application.CreateCustomModelDifferenceStore += h,h => application.CreateCustomModelDifferenceStore -= h)
                .TakeUntilDisposingMainWindow()
                .TransformPattern<CreateCustomModelDifferenceStoreEventArgs,XafApplication>();
        }

        public static IObservable<(XafApplication application, SetupEventArgs e)> WhenSettingUp(this XafApplication application){
            return Observable
                .FromEventPattern<EventHandler<SetupEventArgs>,SetupEventArgs>(h => application.SettingUp += h,h => application.SettingUp -= h)
                .TakeUntilDisposingMainWindow()
                .TransformPattern<SetupEventArgs,XafApplication>();
        }


    }
}