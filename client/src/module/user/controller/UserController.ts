import { headers } from "../const/tableHeaders";
import { ImageModel } from "../domain/model/image";
import { UserModel, UserPagination } from "../domain/model/user";
import { FetchUsersUseCase } from "../domain/useCase/fetchUsersUseCase";
import { Store } from "vuex";

class UserController {
  public options: any;
  public loading = false;

  public user = new UserModel({});
  public userPagination = new UserPagination();

  public headers = headers;
  
  public form = {
    name: undefined,
    email: undefined,
    password: undefined,
    image: new ImageModel({})
  };

  constructor(
    private context: any,
    private store: Store<any>,
    private fetchUsersUseCase: FetchUsersUseCase
  ) {}

  async paginate() {
    this.userPagination = await this.fetchUsersUseCase(this.options, "");
  }

  async signIn() {
    if (this.context.$refs.authForm.$refs.form.validate()) {
      (await this.store.dispatch("signIn", this.form)) &&
        this.handleNavigate("/dashboard");
    }
  }

  async signOut() {
    (await this.store.dispatch("signOut")) && this.handleNavigate("/");
  }

  async signUp() {
    if (this.context.$refs.userForm.$refs.form.validate()) {
      (await this.store.dispatch("signUp", this.user)) &&
        this.handleNavigate("/dashboard");
    }
  }

  private handleNavigate(route: string) {
    this.context.$router.push(route);
  }
}

export { UserController };
