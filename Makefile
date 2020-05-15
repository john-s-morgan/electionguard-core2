.PHONY: build clean demo-c demo-cpp test

BUILD_DEBUG?=true

.EXPORT_ALL_VARIABLES:
ELECTIONGUARD_BUILD_DIR="$(realpath .)/build/electionguard-core"

# Detect operating system
ifeq ($(OS),Windows_NT)
    OPERATING_SYSTEM := Windows
else
    OPERATING_SYSTEM := $(shell uname 2>/dev/null || echo Unknown)
endif

all: environment build

environment:
	wget -O cmake/CPM.cmake https://github.com/TheLartians/CPM.cmake/releases/latest/download/CPM.cmake
ifeq ($(OPERATING_SYSTEM),Darwin)
	brew install cmake
	brew install gmp
	brew install cppcheck
	brew install llvm
	ln -s "$(brew --prefix llvm)/bin/clang-tidy" "/usr/local/bin/clang-tidy"
endif
ifeq ($(OPERATING_SYSTEM),Linux)
    
endif
ifeq ($(OPERATING_SYSTEM),Windows)
    
endif

ifeq ($(BUILD_DEBUG),true)
build: build-debug
else
build: build-release
endif

build-debug: clean
ifeq ($(OPERATING_SYSTEM),Windows)
	cmake -S . -B $(ELECTIONGUARD_BUILD_DIR) -G "MSYS Makefiles" -DCMAKE_BUILD_TYPE=Debug -DBUILD_SHARED_LIBS=ON
else
	cmake -S . -B $(ELECTIONGUARD_BUILD_DIR) -DCMAKE_BUILD_TYPE=Debug -DBUILD_SHARED_LIBS=ON
endif
	cmake --build $(ELECTIONGUARD_BUILD_DIR)

build-release: clean
ifeq ($(OPERATING_SYSTEM),Windows)
	cmake -S . -B $(ELECTIONGUARD_BUILD_DIR) -G "MSYS Makefiles" -DCMAKE_BUILD_TYPE=Release -DBUILD_SHARED_LIBS=ON -DUSE_STATIC_ANALYSIS=ON
else
	cmake -S . -B $(ELECTIONGUARD_BUILD_DIR) -DCMAKE_BUILD_TYPE=Release -DBUILD_SHARED_LIBS=ON -DUSE_STATIC_ANALYSIS=ON
endif
	cmake --build $(ELECTIONGUARD_BUILD_DIR)

clean:
	if [ -d "build" ]; then rm -rf ./build/*; fi
	if [ ! -d "build" ]; then mkdir build; fi
	if [ ! -d "$($(ELECTIONGUARD_BUILD_DIR))" ]; then mkdir $(ELECTIONGUARD_BUILD_DIR); fi
	if [ ! -d "build/apps" ]; then mkdir build/apps; fi
	if [ ! -d "build/apps/demo_in_c" ]; then mkdir build/apps/demo_in_c; fi
	if [ ! -d "build/apps/demo_in_cpp" ]; then mkdir build/apps/demo_in_cpp; fi
	
demo-c: build
ifeq ($(OPERATING_SYSTEM),Windows)
	CMAKE_PREFIX_PATH="$(ELECTIONGUARD_BUILD_DIR)" cmake -S apps/demo_in_c -B build/apps/demo_in_c -G "MSYS Makefiles"
	cmake --build build/apps/demo_in_c --target DemoInC
	PATH=$(ELECTIONGUARD_BUILD_DIR)/src:$$PATH; ./build/apps/demo_in_c/DemoInC
else
	ElectionGuard_DIR=$(ELECTIONGUARD_BUILD_DIR) cmake -S apps/demo_in_c -B build/apps/demo_in_c
	cmake --build build/apps/demo_in_c --target DemoInC
	./build/apps/demo_in_c/DemoInC
endif

demo-cpp: build
ifeq ($(OPERATING_SYSTEM),Windows)
	CMAKE_PREFIX_PATH="./build/electionguard-core" cmake -S apps/demo_in_cpp -B build/apps/demo_in_cpp -G "MSYS Makefiles"
	cmake --build build/apps/demo_in_cpp --target DemoInCPP
	PATH=$(ELECTIONGUARD_BUILD_DIR)/src:$$PATH; ./build/apps/demo_in_cpp/DemoInCPP
else
	ElectionGuard_DIR=$(ELECTIONGUARD_BUILD_DIR) cmake -S apps/demo_in_cpp -B build/apps/demo_in_cpp
	cmake --build build/apps/demo_in_cpp --target DemoInCPP
	./build/apps/demo_in_cpp/DemoInCPP
endif

format: build
	cd ./build/electionguard-core && $(MAKE) format

sanitize: sanitize-asan sanitize-tsan

sanitize-asan: clean
	cmake -S . -B $(ELECTIONGUARD_BUILD_DIR) -DCMAKE_BUILD_TYPE=Debug -DBUILD_SHARED_LIBS=ON -DUSE_SANITIZER="address;undefined"
	cmake --build $(ELECTIONGUARD_BUILD_DIR)
	ElectionGuard_DIR=$(ELECTIONGUARD_BUILD_DIR) cmake -S apps/demo_in_cpp -B build/apps/demo_in_cpp
	cmake --build build/apps/demo_in_cpp --target DemoInCPP
	./build/apps/demo_in_cpp/DemoInCPP
	ElectionGuard_DIR=$(ELECTIONGUARD_BUILD_DIR) cmake -S apps/demo_in_c -B build/apps/demo_in_c
	cmake --build build/apps/demo_in_c --target DemoInC
	./build/apps/demo_in_c/DemoInC

sanitize-tsan: clean
	cmake -S . -B $(ELECTIONGUARD_BUILD_DIR) -DCMAKE_BUILD_TYPE=Debug -DBUILD_SHARED_LIBS=ON -DUSE_SANITIZER="thread"
	cmake --build $(ELECTIONGUARD_BUILD_DIR)
	ElectionGuard_DIR=$(ELECTIONGUARD_BUILD_DIR) cmake -S apps/demo_in_cpp -B build/apps/demo_in_cpp
	cmake --build build/apps/demo_in_cpp --target DemoInCPP
	./build/apps/demo_in_cpp/DemoInCPP
	ElectionGuard_DIR=$(ELECTIONGUARD_BUILD_DIR) cmake -S apps/demo_in_c -B build/apps/demo_in_c
	cmake --build build/apps/demo_in_c --target DemoInC
	./build/apps/demo_in_c/DemoInC

test: clean
ifeq ($(OPERATING_SYSTEM),Windows)
	cmake -S . -B $(ELECTIONGUARD_BUILD_DIR) -G "MSYS Makefiles" -DCMAKE_BUILD_TYPE=Debug -DBUILD_SHARED_LIBS=ON -DOPTION_ENABLE_TESTS=ON -DCODE_COVERAGE=ON -DUSE_STATIC_ANALYSIS=ON
	cmake --build $(ELECTIONGUARD_BUILD_DIR)
	PATH=$(ELECTIONGUARD_BUILD_DIR)/src:$$PATH; $(ELECTIONGUARD_BUILD_DIR)/test/ElectionGuardTests
else
	cmake -S . -B $(ELECTIONGUARD_BUILD_DIR) -DCMAKE_BUILD_TYPE=Debug -DBUILD_SHARED_LIBS=ON -DOPTION_ENABLE_TESTS=ON -DCODE_COVERAGE=ON -DUSE_STATIC_ANALYSIS=ON
	cmake --build $(ELECTIONGUARD_BUILD_DIR)
	$(ELECTIONGUARD_BUILD_DIR)/test/ElectionGuardTests
endif