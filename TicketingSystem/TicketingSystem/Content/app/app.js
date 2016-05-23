(function(angular) {
    var app = angular.module('app', ['app.controllers', 'app.services', 'ui.router', 'login', 'register', 'angularModalService', 'angularjs-dropdown-multiselect']);
    app.controller('Controller', function ($scope, ModalService) {
    

     
        $scope.show = function (creation) {
         
            
            ModalService.showModal({
                templateUrl: 'addEditTask.html',
                controller: "ModalController"
            }).then(function (modal) {
                modal.element.modal();
                modal.close.then(function (result) {
                    $scope.message = "You said " + result;
                });
            });
        };

    });
    app.controller('ModalController', function ($scope, close) {
        
        console.log($scope.$parent);
        $scope.close = function (result) {
            close(result, 500); // close, but give 500ms for bootstrap to animate
        };

    });
	app.config(function($stateProvider, $urlRouterProvider) {
	    $urlRouterProvider.otherwise('/dashboard');
	    $stateProvider
	    .state('dashboard', {
			url: '/dashboard',
			templateUrl: 'partials/dashboard.html',
			controller: 'DashboardCtrl'
	    })
	    .state('login', {
	        url: '/login',
	        templateUrl: 'users/login.html',
	        controller: 'loginCtrl'
	    })
        .state('register', {
            url: '/register',
            templateUrl: 'users/register.html',
            controller: 'registerCtrl'
        })
	    .state('tasks', {
	        url: '/projects/:id/tasks',
	        templateUrl: 'tasks/views/tasksView.html',
	        controller: 'TasksCtrl'
	    })
        .state('taskDetails', {
            url: '/projects/:id/tasks/:taskId',
            templateUrl: 'tasks/views/taskDetailsView.html',
            controller: 'TasksCtrl'
	    });
	})
    .run(run);;

	function run($rootScope, $http, $location, $localStorage, AuthenticationService, $state) {
	    //postavljanje tokena nakon refresh
	    if ($localStorage.currentUser) {
	        $http.defaults.headers.common.Authorization = $localStorage.currentUser.token;
	    }

	    // ukoliko poku�amo da odemo na stranicu za koju nemamo prava, redirektujemo se na login
	    $rootScope.$on('$stateChangeSuccess', function (event, toState, toParams, fromState, fromParams) {
	        var publicStates = ['login', 'dashboard', 'register'];
	        var restrictedState = publicStates.indexOf(toState.name) === -1;
	        if (restrictedState && !AuthenticationService.getCurrentUser()) {
	            $state.go('login');
	        }
	    });

	    $rootScope.logout = function () {
	        AuthenticationService.logout();
	    }

	    $rootScope.getCurrentUserRole = function () {
	        if (!AuthenticationService.getCurrentUser()) {
	            return undefined;
	        }
	        else {
	            return AuthenticationService.getCurrentUser().role;
	        }
	    }
	    $rootScope.isLoggedIn = function () {
	        if (AuthenticationService.getCurrentUser()) {
	            return true;
	        }
	        else {
	            return false;
	        }
	    }
	    $rootScope.getCurrentState = function () {
	        return $state.current.name;
	    }
	}


}(angular));