﻿using System.Collections;
using System.Collections.Generic;
using Sample.Pages;
using Sample.Services;
using Sample.ViewModel;
using Xamarin.Forms;
using Xamarin.Platform;
using Xamarin.Platform.Handlers;
using RegistrarHandlers = Xamarin.Platform.Registrar;

namespace Sample
{
	public class Platform
	{
		static bool HasInit;

		public static IWindow GetWindow()
		{
			return new MainWindow(new MainPage(new MainPageViewModel(new List<ITextService> { new TextService() })));
		}

		public static void Init()
		{
			if (HasInit)
				return;

			HasInit = true;

			//RegistrarHandlers.Handlers.Register<Layout, LayoutHandler>();

			RegistrarHandlers.Handlers.Register<Button, ButtonHandler>();
			RegistrarHandlers.Handlers.Register<Slider, SliderHandler>();
			RegistrarHandlers.Handlers.Register<VerticalStackLayout, LayoutHandler>();
			RegistrarHandlers.Handlers.Register<HorizontalStackLayout, LayoutHandler>();
			RegistrarHandlers.Handlers.Register<Xamarin.Forms.FlexLayout, LayoutHandler>();
			RegistrarHandlers.Handlers.Register<Xamarin.Forms.StackLayout, LayoutHandler>();
			//RegistrarHandlers.Handlers.Register<Entry, EntryHandler>();
			RegistrarHandlers.Handlers.Register<Label, LabelHandler>();
		}


		void RegisterLegacyRendererAgainstFormsControl()
		{
#if MONOANDROID && !NET6_0

			// register renderer with old registrar so it can get shimmed
			// This will move to some extension method
			Xamarin.Forms.Internals.Registrar.Registered.Register(
				typeof(Xamarin.Forms.Button),
				typeof(Xamarin.Forms.Platform.Android.FastRenderers.ButtonRenderer));

			// This registers the shim against the handler registrar
			// So when the handler.registrar returns the RendererToHandlerShim
			// Which will then forward the request to the old registrar
			Registrar.Handlers.Register<Xamarin.Forms.Button, RendererToHandlerShim>();

#endif
		}
	}
}