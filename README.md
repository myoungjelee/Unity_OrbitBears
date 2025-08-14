# 🐻 Unity Orbit Bears

![Unity](https://img.shields.io/badge/Unity-2022.x-black.svg?style=flat-square&logo=unity)
![WebGL](https://img.shields.io/badge/Platform-WebGL-orange.svg?style=flat-square)
![URP](https://img.shields.io/badge/Render%20Pipeline-URP-blue.svg?style=flat-square)
![Development Status](https://img.shields.io/badge/Status-Active-brightgreen.svg?style=flat-square)

우주 공간에서 궤도를 따라 움직이는 귀여운 곰들과 함께하는 물리 기반 퍼즐 게임입니다.

## 📋 프로젝트 개요

Unity로 제작된 **Orbit Bears**는 중력과 궤도 역학을 활용한 물리 퍼즐 게임입니다. 플레이어는 우주 곰들이 안전하게 목적지에 도착할 수 있도록 궤도를 조작하고 중력을 이용해 퍼즐을 해결합니다.

### 🎯 주요 특징

- **🌌 우주 테마**: 아름다운 우주 배경과 행성들
- **🐻 귀여운 캐릭터**: 사랑스러운 우주 곰 캐릭터들
- **⚡ 물리 엔진**: 실제 궤도 역학과 중력 시뮬레이션
- **🧩 퍼즐 게임**: 두뇌를 자극하는 궤도 퍼즐

## 🛠️ 기술 스택

### 엔진 & 렌더링

- **Unity 2022.x**
- **Universal Render Pipeline (URP)**

### 플랫폼

- **Windows Standalone**
- **모바일 지원 가능**

### 주요 에셋 & 라이브러리

- `JellyIcons` - UI 아이콘 시스템
- `Planets with Space Background` - 우주 배경 에셋
- `TextMesh Pro` - 고품질 텍스트 렌더링
- `Universal RP` - 최적화된 렌더링 파이프라인

## 📁 프로젝트 구조

```
Assets/
├── 📂 Audios/          # 배경음악 및 효과음
├── 📂 Editor/          # 에디터 확장 스크립트
├── 📂 Fonts/           # 폰트 파일
├── 📂 JellyIcons/      # UI 아이콘 시스템
├── 📂 Materials/       # 머티리얼
├── 📂 Models/          # 3D 모델 (곰, 우주선 등)
├── 📂 Particles/       # 파티클 효과
├── 📂 Planets/         # 행성 및 우주 배경
├── 📂 Prefabs/         # 게임 오브젝트 프리팹
├── 📂 Resources/       # 런타임 리소스
├── 📂 Scenes/          # 게임 씬
├── 📂 Scriptabledata/  # 스크립터블 오브젝트 데이터
├── 📂 Scripts/         # C# 게임 로직
├── 📂 Sprites/         # 2D 스프라이트 이미지
└── 📂 Textures/        # 텍스처 파일
```

## 🎮 주요 기능

### 물리 기반 게임플레이

- **중력 시뮬레이션**: 행성의 중력이 곰들의 궤도에 영향
- **궤도 역학**: 실제 우주 물리학을 단순화한 궤도 시스템
- **탄성 충돌**: 행성과 장애물과의 물리적 상호작용
- **momentum 보존**: 실감나는 물리 법칙 적용

### 레벨 디자인

- **점진적 난이도**: 쉬운 튜토리얼부터 복잡한 퍼즐까지

### 사용자 경험

- **직관적 컨트롤**: 마우스/터치로 간단한 조작
- **시각적 피드백**: 궤도 예측선과 힘의 방향 표시
- **반응형 UI**: 다양한 화면 크기 지원

## 🚀 플레이 방법

### 기본 조작

1. **마우스 드래그**: 곰의 초기 속도와 방향 설정
2. **클릭**: 곰 발사
3. **R키**: 레벨 재시작
4. **ESC**: 메뉴 열기

### 게임 목표

- 🎯 **목적지 도달**: 곰을 안전하게 목표 지점으로 안내
- ⭐ **별 수집**: 최고레벨 별 완성
- 🕐 **시간 단축**: 최소한의 시도로 클리어
- 🏆 **완벽한 궤도**: 가장 효율적인 경로 찾기

## 🎯 개발 하이라이트

### 물리 시스템

```csharp
// 중력 계산 예시
Vector2 gravityForce = mass * gravityStrength *
                      (planetPosition - bearPosition).normalized /
                      Vector2.Distance(planetPosition, bearPosition);
```

### 궤도 예측

- 실시간 궤도 경로 시뮬레이션
- 베지어 곡선을 이용한 부드러운 궤도 표시
- 충돌 예측 시스템

### 레벨 데이터

- ScriptableObject 기반 레벨 데이터
- JSON 직렬화를 통한 레벨 저장/로드
- 모듈러 레벨 디자인 시스템

## 📊 개발 진행상황

- [x] 기본 물리 시스템 구현
- [x] 곰 캐릭터 및 애니메이션
- [x] 궤도 예측 시스템
- [x] 레벨 디자인 툴
- [x] UI/UX 시스템
- [x] 웹 빌드 최적화
- [x] 모바일 터치 지원
- [x] 사운드 시스템

### 주요 화면

- 🏠 메인 메뉴
- 🎮 게임플레이 화면
- 🏆 결과 화면
- ⚙️ 설정 메뉴

### 성능 최적화

- URP 렌더링 파이프라인 사용
- Texture 압축 및 최적화
- Audio 압축 설정
- 코드 최적화 (Object Pooling 등)

### 커스텀 에디터

프로젝트에는 레벨 디자인을 위한 커스텀 에디터 툴이 포함되어 있습니다.

## 🤝 기여하기

### 기여 방법

- 새로운 레벨 디자인
- 버그 수정 및 최적화
- UI/UX 개선
- 새로운 게임 메커닉 아이디어

### 이슈 리포트

버그나 개선사항을 발견하시면 GitHub Issues에 등록해 주세요.

## 🏆 랭킹 시스템

게임에는 로컬 랭킹 시스템이 구현되어 있습니다:

- 레벨별 최고 점수
- 총 플레이 시간
- 완료한 레벨 수
- 수집한 별의 총 개수

## 📄 라이선스

이 프로젝트는 개인 학습 및 포트폴리오 목적으로 제작되었습니다.

### 사용된 외부 에셋

- JellyIcons (Unity Asset Store)
- Planets with Space Background in Flat Style (Unity Asset Store)
- Universal Render Pipeline (Unity Technologies)

## 📞 연락처

**개발자**: myoungjelee  
**GitHub**: [@myoungjelee](https://github.com/myoungjelee)

---

_우주 곰들과 함께 떠나는 궤도 여행을 즐겨보세요!_ 🚀🐻⭐
